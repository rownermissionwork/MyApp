using Account.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure
{
    public class JwtService(IConfiguration config) : IJwtService
    {
        private readonly IConfiguration _config = config;

        public string GenerateToken<T>(T detail) where T : class
        {
            try {
                var claims = new List<Claim>();
                var type = detail.GetType();
                var props = type.GetProperties();

                foreach (var prop in props)
                {
                    claims.Add(new Claim(prop.Name, prop.GetValue(detail)?.ToString() ?? string.Empty));
                }


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"].ToString()));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch {
                throw;
            }
        }
    }
}
