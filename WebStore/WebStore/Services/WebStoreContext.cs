using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebStore.Models;

namespace WebStore.Services
{
    public class WebStoreContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        

        public WebStoreContext() : base("name=DbConnection")
        {
            this.Orders = this.Set<Order>();
            this.Customers = this.Set<Customer>();
        }
    }
}