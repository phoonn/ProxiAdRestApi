using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebStore.Models;
using WebStore.Services;
using Newtonsoft.Json;
using System.Text;

namespace WebStore.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private UnitOfWork unit;
        //[Route("")]
        //public Order[] GetAllOrders()
        //{
        //    using( unit = new UnitOfWork())
        //    {
        //        return unit.OrderRepo.Get(null, null, string.Empty, 0).ToArray();
        //    }
        //}

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Customer customer)
        {
            try
            {
                using (unit = new UnitOfWork())
                {
                    Order[] orders = unit.OrderRepo.Get(o => o.CustomerId == customer.Id, null, string.Empty, 0).ToArray();
                    var message = Request.CreateResponse(HttpStatusCode.Found);
                    if (orders!=null)
                    {
                        var json = JsonConvert.SerializeObject(orders);
                        message.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                    }
                    message.Headers.Location = new Uri(Request.RequestUri + @"/"+customer.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("{id:int}")]
        public Order[] GetFromId(int id)
        {
            using (unit = new UnitOfWork())
            {
                 return unit.OrderRepo.Get(o=> o.CustomerId==id, null, string.Empty, 0).ToArray();
            }

        }


        [Route("latest/{count:int}")]
        public Order[] GetSomeOrders(int count)
        {
            using ( unit = new UnitOfWork())
            {
                if (count <= 0)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                    //HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,"This Item Does not Exist"));
                }
                else
                    return unit.OrderRepo.Get(null, query => query.OrderByDescending(order => order.Id), string.Empty, count).ToArray();
            }
                
        }
    }
}
