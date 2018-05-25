using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMoneyManager.Models;
using System.Linq;

namespace MyMoneyManagerCoreWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Movimenti")]
    public class MovimentiController : Controller
    {
        // GET: api/Movimenti
        [HttpGet]
        public IEnumerable<Movimenti> Get()
        {
            MyMoneyManagerEntities context = new MyMoneyManagerEntities();
            return context.Movimenti.ToList();
        }

        // GET: api/Movimenti/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Movimenti
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Movimenti/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
