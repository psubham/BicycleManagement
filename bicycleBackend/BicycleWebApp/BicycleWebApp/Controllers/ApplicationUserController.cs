using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BicycleWebApp.UserException;
using BicycleWebApp.Models;
using BicycleWebApp.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using BicycleWebApp.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace BicycleWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandler]
    public class ApplicationUserController : ControllerBase
    {
        private ApplicationUserService _applicationUserService;
        public ApplicationUserController(ApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }
        #region register
        [HttpPost]
        [Route("Register")]
        public  async Task<Object> PostApplication(ApplicationUserData data)
        {
           var res= await this._applicationUserService.RegisterAsync(data);
            return Ok(res);
        }
        #endregion

        #region login
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<Object>> Login(LoginModel loginModel)
        {
            var res = await this._applicationUserService.Login(loginModel);
           
            return Ok(res);
        }
        #endregion

        #region usernameexists
        [HttpPost]
        [Route("UsernameExist")]
        public async Task<ActionResult<bool>> UsernameExist(string UserName)
        {
            return Ok(await this._applicationUserService.UsernameExist(UserName));
        }
        #endregion

        #region get user roles
        [HttpGet("GetUserRoles")]
        public async Task<ActionResult<Object>> GetUserRoles()
        {
            return await this._applicationUserService.GetUserRoles();
        }
        #endregion

        #region set user role
        [HttpPost("SetUserRole")]
        [Authorize(Roles ="admin")]
        public void SetUserRoles(string username, string roles,string newRole)
        {
            this._applicationUserService.SetUserRoles(username,roles,newRole);
        }
        #endregion

        #region get roles
        [HttpGet("GetRoles")]
        [Authorize(Roles ="admin")]
        public async Task<Object> GetRoleList()
        {
            return await this._applicationUserService.GetRolesList();
        }
        #endregion

    }
}