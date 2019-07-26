using App.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App.DatabaseContext
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Need to install package "Microsoft.Extensions.Configuration.FileExtensions"
                .AddJsonFile(@Directory.GetCurrentDirectory() + ApplicationConstant.APP_SETTING_PATH) // Need to install package "Microsoft.Extensions.Configuration.Json"
                .Build();

            string connectionString = configuration.GetConnectionString(ApplicationConstant.DatabaseContext.MASTER_CONNECTION_STRING);

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql(connectionString);

            return new ApplicationDbContext(builder.Options);
        }
    }
}
