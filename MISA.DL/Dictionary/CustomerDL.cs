using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.BO;
namespace MISA.DL
{
    public class CustomerDL
    {
        private MISACUKCUKAPIContext db = new MISACUKCUKAPIContext();
        /// <summary>
        /// lấy all 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return db.Customers;
        }
        /// <summary>
        /// lấy theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomerByID(Guid id)
        {
            Customer customer = db.Customers.Find(id);
            return customer;
        }
        /// <summary>
        /// thêm kh 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
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
        public bool UpdateCustomer(Guid id, Customer customer)
        {
            if (!CustomerExists(id))
            {
                return false;
            }
            try
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// xóa kh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object DeleteCustomer(Guid id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            else
            {
                return null;
            }

            return customer;

        }
        // hàm mở rộng

        private bool CustomerExists(Guid id)
        {
            return db.Customers.Count(e => e.CID == id) > 0;
        }
    }
}
