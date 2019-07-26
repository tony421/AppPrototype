using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.DatabaseContext;
using Microsoft.Extensions.Configuration;

namespace App.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
        public ActionResult<IEnumerable<string>> GetMaster()
        {
            var names = _masterContext.Corporates.Select(s => s.Name)?.ToArray();
            return names;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetProduction()
        {
            var names = _prodContext.Stores.Select(s => s.Name)?.ToArray();
            return names;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
