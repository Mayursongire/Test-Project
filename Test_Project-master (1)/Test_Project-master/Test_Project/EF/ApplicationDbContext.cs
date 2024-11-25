using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Test_Project.Models;

namespace Test_Project.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        public ApplicationDbContext() : base("test")
        {

        }

        
    }
}