using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapPointApi.model;
using MapPointApi.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MapPointApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly HubService hubService;

        public HubController(HubService hubService)
        {
            this.hubService = hubService;
        }

        // GET: api/Hub
        [HttpGet]
        public IEnumerable<Hub> Get()
        {
            return this.hubService.GetHubs();
        }
        // GET: api/Hub/5
        [HttpGet("{id}", Name = "GetHub")]
        public async Task<Hub> GetHub([FromHeader]int Id)
        {
            return await this.hubService.GetHub(Id);
        }

     


        // POST: api/Hub
        [HttpPost]
        public async Task Post([FromBody] Hub value)
        {
            await this.hubService.AddHub(value);
        }

        [HttpPost("IsHub")]
        public async Task<bool> Post(int Id)
        {
            return await this.hubService.Ishub(Id);
        }

        // PUT: api/Hub/5
        [HttpPut("UpdateHub")]
        public async Task UpdateHub( Hub value)
        {
            await this.hubService.UpdateHub(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await this.hubService.DeleteHub(id);
        }
    }
}
