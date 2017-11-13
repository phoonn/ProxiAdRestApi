using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebStore.Models
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> CustomerOrders { get; set; }
    }
}