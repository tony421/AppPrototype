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
    public class DesignTimeMasterDbContextFactory : DesignTimeDbContextFactoryBase<MasterDbContext>
    {
        public override MasterDbContext Create(DbContextOptions<MasterDbContext> options)
        {
            Console.WriteLine($"Creating a database context. => DbContext: {nameof(MasterDbContext)}");
            return new MasterDbContext(options);
        }

        public override string GetConnectionString()
        {
            return ConstDbContext.CONN_STRING_MASTER;
        }
    }
}
