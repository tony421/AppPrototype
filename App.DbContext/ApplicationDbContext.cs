using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using App.DatabaseContext.Models;
using App.DatabaseContext.Models.Master;

namespace App.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Corporate> Corporates { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);
        }
    }
}
