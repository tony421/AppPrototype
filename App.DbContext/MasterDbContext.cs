using System;
using System.Collections.Generic;
using System.Text;
using App.DatabaseContext.Models.Master;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DatabaseContext
{
    /// <summary>
    /// Adding migration on "Package Manager Console" by command "add-migration InitialMaster -context MasterDbContext -o Migrations/Master"
    /// </summary>
    public class MasterDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public virtual DbSet<Corporate> Corporates { get; set; }

        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);
        }
    }
}
