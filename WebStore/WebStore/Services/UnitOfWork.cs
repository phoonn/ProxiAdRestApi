using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.Models;

namespace WebStore.Services
{
    public class UnitOfWork : IDisposable
    {
        
        private bool disposed = false;

        private WebStoreContext context = new WebStoreContext();
        private BaseRepository<Customer> customerRepo;
        private BaseRepository<Order> orderRepo;


        public BaseRepository<Customer> CustomerRepo
        {
            get
            {
                return this.customerRepo ?? new BaseRepository<Customer>(context);
            }
        }

        public BaseRepository<Order> OrderRepo
        {
            get
            {
                return this.orderRepo ?? new BaseRepository<Order>(context);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}