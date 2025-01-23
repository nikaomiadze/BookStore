using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    public class JWTmanager : IjwtManager
    {
        private readonly IConfiguration config;
        public JWTmanager(IConfiguration config)
        {
            this.config = config;
        }
        public Token GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(config["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID",user.Id.ToString(),ClaimValueTypes.Integer),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokeData = tokenHandler.CreateToken(tokenDescriptor);
            var token = new Token { AccessToken = tokenHandler.WriteToken(tokeData) };
            return token;
        }
    }
}
