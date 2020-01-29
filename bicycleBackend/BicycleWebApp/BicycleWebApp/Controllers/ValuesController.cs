using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BicycleWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BicycleWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private UserManager<ApplicatinUser> _userManager;
        public ValuesController(UserManager<ApplicatinUser> userManager)
        {
            _userManager = userManager;

        }
        // GET api/values
        [HttpGet]
        [Authorize(Roles= "customer")]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "userId").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                
                user.UserName
            };
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
