using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.BO;
namespace MISA.DL
{
   public class CustomerDL
    {
        private MISACUKCUKAPIContext db = new MISACUKCUKAPIContext();
        public IEnumerable<Customer> GetCustomers()
        {
            return db.Customers;
        }
        public Customer GetCustomerByID(Guid id)
        {
            Customer customer = db.Customers.Find(id);
            return customer;
        }
        public bool AddCustomer(Customer customer)
        {
            customer.CID = Guid.NewGuid();
            db.Customers.Add(customer);

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                if (CustomerExists(customer.CID))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }


        // hàm mở rộng

        private bool CustomerExists(Guid id)
        {
            return db.Customers.Count(e => e.CID == id) > 0;
        }
    }
}
