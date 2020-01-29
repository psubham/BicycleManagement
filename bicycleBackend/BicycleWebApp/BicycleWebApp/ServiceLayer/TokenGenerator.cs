using BicycleWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BicycleWebApp.ServiceLayer
{
    public class TokenGenerator
    {
        private ApplicationSettings _applicationSettings;
        public TokenGenerator(IOptions<ApplicationSettings> options)
        {
            _applicationSettings = options.Value;

        }

        public string GetJWTToken(string userId, IEnumerable<string> roles)
        {
            IdentityOptions _options = new IdentityOptions();
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                   {
                       new Claim("userId", userId),

                       new Claim(_options.ClaimsIdentity.RoleClaimType,string.Join(",",roles))
                   }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var securitytoken = tokenhandler.CreateToken(tokendescriptor);
            var Token = tokenhandler.WriteToken(securitytoken);
            string token = JsonConvert.SerializeObject(Token);
            return token;
        }
    }
}
