using App.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App.DatabaseContext
{
    public class DesignTimeProductionDbContextFactory : DesignTimeDbContextFactoryBase<ProductionDbContext>
    {
        public override ProductionDbContext Create(DbContextOptions<ProductionDbContext> options)
        {
            Console.WriteLine($"Creating a database context. => DbContext: {nameof(ProductionDbContext)}");
            return new ProductionDbContext(options);
        }

        public override string GetConnectionString()
        {
            return ConstDbContext.CONN_STRING_PRODUCTION_MASTER;
        }
    }
}
