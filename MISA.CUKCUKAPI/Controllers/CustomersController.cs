using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MISA.BO;
using MISA.DL;

namespace MISA.CUKCUKAPI.Controllers
{
    public class CustomersController : ApiController
    {
        private CustomerDL db = new CustomerDL();
        // GET: api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return db.GetCustomers();
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(Guid id)
        {
            var customer = db.GetCustomerByID(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = db.AddCustomer(customer);
            

            return CreatedAtRoute("DefaultApi", new { id = customer.CID }, customer);
        }


        // hàm của framework

    }
}