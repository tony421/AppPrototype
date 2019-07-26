using App.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DatabaseContext
{
    public class ApplicationDbContextFactory
    {
        public MasterDbContext CreateMasterContext(IConfiguration config)
        {
            if (config != null)
            {
                string connString = config.GetConnectionString(ApplicationConstant.DatabaseContext.MASTER_CONNECTION_STRING);
                if (!string.IsNullOrWhiteSpace(connString))
                {
                    var optionsBuilder = new DbContextOptionsBuilder<MasterDbContext>();
                    optionsBuilder.UseNpgsql(connString);

                    return new MasterDbContext(optionsBuilder.Options);
                }
                else throw new ArgumentNullException(nameof(connString));
            }
            else throw new ArgumentNullException(nameof(config));
        }

        public ProductionDbContext CreateProductionContext(IConfiguration config)
        {
            return CreateProductionContext(config, ApplicationConstant.DatabaseContext.PRODUCTION_MASTER_DATABASE);
        }
        public ProductionDbContext CreateProductionContext(IConfiguration config, string databaseName)
        {
            if (config != null)
            {
                if (!string.IsNullOrWhiteSpace(databaseName))
                {
                    string productionConnFormat = config.GetConnectionString(ApplicationConstant.DatabaseContext.PRODUCTION_FORMAT_CONNECTION_STRING);
                    if (!string.IsNullOrWhiteSpace(productionConnFormat))
                    {
                        string connString = string.Format(productionConnFormat, databaseName);

                        var optionsBuilder = new DbContextOptionsBuilder<ProductionDbContext>();
                        optionsBuilder.UseNpgsql(connString);

                        return new ProductionDbContext(optionsBuilder.Options);
                    }
                    else throw new ArgumentNullException(nameof(productionConnFormat));
                }
                else throw new ArgumentNullException(nameof(databaseName));
            }
            else throw new ArgumentNullException(nameof(config));
            
        }
    }
}
