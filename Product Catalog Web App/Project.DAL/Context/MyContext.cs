using Project.ENTITIES.Models;
using Project.MAP.Options;
using System.Data.Entity;

namespace Project.DAL.Context
{
    public class MyContext : DbContext
    {
        public MyContext() : base("myConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new BrandMap());
            modelBuilder.Configurations.Add(new CableTypeMap());
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CableType> CableTypes { get; set; }
    }
}
