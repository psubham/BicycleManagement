using BicycleWebApp.DataAccessLayer;
using BicycleWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BicycleWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BicycleWebApp.Data.RepositoryLyer
{
    public class ApplicationUserRepo
    {
        private readonly AuthenticationContext db;
        private UserManager<ApplicatinUser> _userManager;
        private readonly ApplicationSettings _applicationSettings;
        public ApplicationUserRepo(AuthenticationContext authenticationContext,
                            UserManager<ApplicatinUser> userManager,
                            SignInManager<ApplicatinUser> signInManager,
                            IOptions<ApplicationSettings> applicationsettings)
        {
            this.db = authenticationContext;
            _userManager = userManager;
            _applicationSettings = applicationsettings.Value;
        }

        public async Task<Object> RegisterAsync(ApplicationUserData data)
        {

            data.Role = "admin";
            var applicationuser = new ApplicatinUser()
            {
                UserName = data.UserName,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,

            };

            // var result = JsonConvert.SerializeObject(await _userManager.CreateAsync(applicationuser, data.Password));
            var result = await _userManager.CreateAsync(applicationuser, data.Password);
            // await _userManager.AddToRoleAsync(applicationuser,);
            await _userManager.AddToRoleAsync(applicationuser, data.Role);
            if (result == null || result.Succeeded == false)
                return false;
            //if(result.Succeeded==true)
            //{
            //    string ctoken = _userManager.GenerateEmailConfirmationTokenAsync(applicationuser).Result;
            //    string ctokenLink=Uri.Action
            //}
            return result;
        }
        public async Task<IEnumerable<string>> Login(ApplicatinUser applicatinUser, string Password)
        {
            //var user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (await _userManager.CheckPasswordAsync(applicatinUser, Password))
            {
                var role = await _userManager.GetRolesAsync(applicatinUser);


                return role;

            }
            else
                return null;
        }

        public async Task<ApplicatinUser> IsUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        public async Task<Object> GetUser(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FirstName,
                user.Email,
                user.LastName,
                user.UserName
            };
        }

        //public async Task<ApplicatinUser> IsSignIn()
        //{
        //    return await this._signInManager.IsSignedIn();
        //}
        public async Task<IEnumerable<ViewUserRole>> GetUserRoles()
        {
            //List<ViewUserRole> UserRoles = new List<ViewUserRole>();
            //var userdelrole = db.Roles.Include(x => x.Users).ToList();
            var res = db.Roles.ToList();
            var userswithroles = await (from user in db.Users
                                        join
             userrole in db.UserRoles  /*user.id equals userrole.id*/
            on user.Id equals userrole.UserId
                                        join roles in db.Roles on
                                        userrole.RoleId equals roles.Id
                                        select new ViewUserRole()
                                        {
                                            UserName = user.UserName,
                                            RoleName = roles.Name
                                        }).ToListAsync();
            return userswithroles;

        }
        public void SetUserRoles(string username, string roles, string newRole)
        {
            try
            {
                var res = db.Roles.ToList();
                var setuserroles = (from user in db.Users
                                    join userrole in db.UserRoles
                                    on user.Id equals userrole.UserId
                                    join roless in db.Roles on
                                    userrole.RoleId equals roless.Id
                                    where user.UserName == username && roless.Name == roles
                                    select userrole).FirstOrDefault();
                var id = (from role in db.Roles
                          where role.Name == newRole
                          select role.Id).FirstOrDefault();

                db.UserRoles.Remove(setuserroles);
                db.SaveChanges();
                setuserroles.RoleId = id;
                db.UserRoles.Add(setuserroles);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }
        public async Task<Object> GetRoleList()
        {
            var rolelist = (from rolesinr in db.Roles 
                            where rolesinr.Name != "admin"
                            select rolesinr.Name).ToList();
            return rolelist;
        }
    }
}
