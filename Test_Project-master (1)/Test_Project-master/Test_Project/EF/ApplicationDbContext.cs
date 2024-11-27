using System.Data.Entity;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Enable Cascade Delete between Category and Product
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Category) 
                .WithMany()                   
                .HasForeignKey(p => p.ProductCategoryId) 
                .WillCascadeOnDelete(true);   
        }
    }
}
