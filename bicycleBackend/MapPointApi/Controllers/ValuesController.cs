using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapPointApi.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MapPointApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        List<Map> map = new List<Map>();
        //ValuesController()
        //{
        //    this.map ;

        //}
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Map>> Get()
        {

            this.map.Add(new Map(12.9581056, 77.6421376));
            this.map.Add(new Map(12.86, 77.6421376));
            this.map.Add(new Map(12.56, 77.32));
            this.map.Add(new Map(12.962245990540822, 77.64690120321052));
            this.map.Add(new Map(12.949406373400873, 77.64196593862312));
            return map;
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
