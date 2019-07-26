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
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public abstract TContext Create(DbContextOptions<TContext> options);

        public abstract string GetConnectionString();

        public TContext CreateDbContext(string[] args)
        {
            string environmentName = Environment.GetEnvironmentVariable(ApplicationConstant.EnvironmentVariableNames.ASPNETCORE_ENV);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Need to install package "Microsoft.Extensions.Configuration.FileExtensions"
                .AddJsonFile(@Directory.GetCurrentDirectory() + ApplicationConstant.APP_SETTING_PATH, true) // Need to install package "Microsoft.Extensions.Configuration.Json"
                .AddJsonFile(@Directory.GetCurrentDirectory() + string.Format(ApplicationConstant.APP_SETTING_PATH, environmentName), true)
                .Build();

            string connectionString = configuration.GetConnectionString(GetConnectionString());
            Console.WriteLine($"Establishing a connection. => Connection string: {connectionString}");

            var builder = new DbContextOptionsBuilder<TContext>();
            builder.UseNpgsql(connectionString);

            return Create(builder.Options);
        }
    }
}
