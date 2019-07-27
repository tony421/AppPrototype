using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace App.API.Controllers
{
    public class ValuesController : ControllerBase
    {
        private MasterDbContext _masterContext;
        private ProductionDbContext _prodContext;

        //public ValuesController(ApplicationDbContext context)
        public ValuesController(IConfiguration config, ApplicationDbContextFactory contextFactory)
        {
            _masterContext = contextFactory.CreateMasterContext(config);
            _prodContext = contextFactory.CreateProductionContext(config);

        }

        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public ActionResult<IEnumerable<string>> GetMaster()
        {
            var names = _masterContext.Corporates.Select(s => s.Name)?.ToArray();
            return names;
        }

        [HttpGet]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public ActionResult<IEnumerable<string>> GetProduction()
        {
            var names = _prodContext.Stores.Select(s => s.Name)?.ToArray();
            return names;
        }
    }
}
