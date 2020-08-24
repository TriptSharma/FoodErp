using FoodErp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace FoodErp.Controllers.Api
{
    public class CustomersController : ApiController
    {
        //GET api/customers
        public IHttpActionResult GetCustomers()
        {

            IList<CustomerViewModel> customers = null;
            
            using (var _context = new FoodiesEntities())
            {
                customers = _context.Customers.Include("Store")
                            .Select(s => new CustomerViewModel()
                            {
                                CustomerId = s.CustomerId,
                                CustomerName = s.CustomerName,
                                Purchase = s.Purchase,
                                Store = s.StoreId == null? null : new StoreViewModel()
                                {

                                    StoreId = s.Store.StoreId,
                                    StoreName = s.Store.StoreName,
                                    Revenue = s.Store.Revenue
                                }
                            }
                            ).ToList<CustomerViewModel>();
            };
            return Ok(customers);
        }

        [HttpPost]
        public IHttpActionResult Create(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            using(var _context = new FoodiesEntities())
            {
                _context.Customers.Add(new Customer()
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    Purchase = customer.Purchase,
                    StoreId = customer.Store.StoreId
                });
                _context.SaveChanges();
            }

            return Created("custoemrs/"+customer.CustomerId.ToString(), customer);
        }
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            if (id <= 0)
                return BadRequest();

            Customer customerInDb = null;
            using (var _context = new FoodiesEntities())
            {
                customerInDb = _context.Customers.Where(s => s.CustomerId == id).FirstOrDefault();

                if (customerInDb == null)
                    return NotFound();

                _context.Customers.Remove(customerInDb);
                _context.SaveChanges();
            }
            return Ok(customerInDb);
        }
    }
}
