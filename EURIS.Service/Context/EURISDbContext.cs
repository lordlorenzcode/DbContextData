
using EURIS.Entities;
using EURIS.Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EURIS.Service.Context
{
    public class EURISDbContext : DbContext
    {
        public EURISDbContext()
             : base("DefaultConnection")
        {
        }


        // DOMAIN 
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<ProductsCatalogs> ProductsCatalogs { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}
