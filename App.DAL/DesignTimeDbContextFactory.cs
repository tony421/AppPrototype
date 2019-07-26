using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App.DAL
{
    public class DesignTimeDbContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
    {
        /*public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Need to install package "Microsoft.Extensions.Configuration.FileExtensions"
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json") // Need to install package "Microsoft.Extensions.Configuration.Json"
                .Build();

            string connectionString = configuration.GetConnectionString("corporate2");

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql(connectionString);

            return new ApplicationDbContext(builder.Options);
        }*/

        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
    }
}
