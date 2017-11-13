using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebStore.Models;
using WebStore.Services;

namespace WebStore.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomerController : ApiController
    {
        private UnitOfWork unit;
        [Route("")]
        public Customer[] GetAllCustomers()
        {
            using (unit = new UnitOfWork())
            {
                return unit.CustomerRepo.Get(null, null, String.Empty, 0).ToArray();
            }
        }

        [Route("{id:int}")]
        public Customer GetCustomer(int id)
        {
            using (unit = new UnitOfWork())
            {
                return unit.CustomerRepo.GetByID(id);
            }
        }
    }
}
