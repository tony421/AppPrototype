using App.DatabaseContext.Models.Production;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DatabaseContext
{
    /// <summary>
    /// Adding migration on "Package Manager Console" by command "add-migration InitialProduciton -context ProductionDbContext -o Migrations/Production"
    /// </summary>
    public class ProductionDbContext :DbContext
    {
        public virtual DbSet<Store> Stores { get; set; }

        public ProductionDbContext(DbContextOptions<ProductionDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
