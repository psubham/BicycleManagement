using BicycleWebApp.Data.RepositoryLyer;
using BicycleWebApp.UserException;
using BicycleWebApp.Models;
using System;
using System.Threading.Tasks;
using BicycleWebApp.Data.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace BicycleWebApp.ServiceLayer
{
    public class ApplicationUserService
    {
        ApplicationUserRepo applicationUserRepo;
        TokenGenerator tokenGenerator;
         public ApplicationUserService(ApplicationUserRepo applicationUserRepo,TokenGenerator tokenGenerator)
        {
            this.applicationUserRepo = applicationUserRepo;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<Object> RegisterAsync(ApplicationUserData data)
        {
            var result = await this.applicationUserRepo.IsUserName(data.UserName);
            if(result!=null)
            {
                // duplicateUser
                throw new DuplicateElement("User is already Exist");
            }
            var user=await this.applicationUserRepo.RegisterAsync(data);
            if (user == null)
                throw new ElementCannotCreated("Not able to create user");
            return user;
        }
        public async Task<bool> UsernameExist(string UserName)
        {
            var user = await this.applicationUserRepo.IsUserName(UserName);
            if (user == null)
            {
                return false;
            }
            return true;

        }

        public async Task<Object> GetUser(string userId)
        {
            return this.applicationUserRepo.GetUser(userId); 
        }
        public async Task<Object> Login(LoginModel loginModel)
        {
            
            var user=await this.applicationUserRepo.IsUserName(loginModel.UserName);
            if (user == null)
            {
                //user expception
                throw new LoginException(" Username is incorrect");
            }
            var result=await this.applicationUserRepo.Login(user, loginModel.Password);
            if(result==null)
            {
                //passwordException
                throw new LoginExceptionPassword(" Password incorrect");
            }
            try
            {

                var Token = tokenGenerator.GetJWTToken(loginModel.UserName,result);
                LoginResponse responseObject = new LoginResponse()
                {
                    UserName = loginModel.UserName,
                    Role = string.Join(",", result),
                    token = Token
                };
                return responseObject;
            }
            catch
            {
                throw new Exception("Cannot generate token");
            }
            //return (result);
        }
        public async Task<Object> GetUserRoles()
        {
            return await this.applicationUserRepo.GetUserRoles();
        }
        public void SetUserRoles(string username,string roles,string newRole)
        {
            this.applicationUserRepo.SetUserRoles(username,roles,newRole);
        }
        public async Task<Object> GetRolesList()
        {
            return await this.applicationUserRepo.GetRoleList();
        }
 
    }
}
