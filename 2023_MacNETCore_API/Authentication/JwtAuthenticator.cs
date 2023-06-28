using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _2023_MacNETCore_API.Models;
using _2023_MacNETCore_API.Interfaces;

namespace _2023_MacNETCore_API.Authentication
{
	public class JwtAuthenticator : IJwtAuthenticator
    {
        private readonly IConfiguration _config;

        public JwtAuthenticator(IConfiguration config)
		{
            _config = config;
		}


        public string GenerateToken(JwtClients user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            /*var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username!),
                new Claim(ClaimTypes.Role,user.Role!)
            };*/
            var token = new JwtSecurityToken(
                 issuer: _config["Jwt:Issuer"],
                 audience: _config["Jwt:Audience"],
                 //claims,
                 expires: DateTime.Now.AddMinutes(15),
                 signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}

