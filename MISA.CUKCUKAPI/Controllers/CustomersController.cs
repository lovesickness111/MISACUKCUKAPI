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
        /// <summary>
        /// hàm lấy tất cả khách hàng
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return db.GetCustomers();
        }

        // GET: api/Customers/5
        /// <summary>
        /// hàm lấy kh theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// hàm thêm khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
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
        // PUT: api/Customers/5
        /// <summary>
        /// hàm sửa thông tin khách hàng
        /// </summary>
        /// <param name="id">id của khách hàng cần sửa</param>
        /// <param name="customer">thông tin kh</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(Guid id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CID)
            {
                return BadRequest();
            }

            try
            {
                var result = db.UpdateCustomer(id, customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (id != customer.CID)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        // DELETE: api/Customers/5
        /// <summary>
        /// xóa kh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(Guid id)
        {
            var customer = db.DeleteCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
                return Ok(customer);
        }

        // hàm của framework

    }
}