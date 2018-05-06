using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class SalesContext:DbContext
    {

        public SalesContext() : base("name=CrudDB")
        {
            Database.SetInitializer<SalesContext>(new DropCreateDatabaseIfModelChanges<SalesContext>());
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSold> ProdcutSales { get; set; }
        public DbSet<Store> Stores { get; set; }
    }
}