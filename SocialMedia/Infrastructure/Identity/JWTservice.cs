using API.Domain.Entites;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class JWTservice : IJWTService
    {
        private readonly IConfiguration _config;

        public JWTservice(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<IEnumerable<Claim>> generateClaimsAsync(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            return claims;
        }

        public async Task<string> generateTokenAsync(User user, IEnumerable<Claim> claims)
        {
            SecurityKey securityKey = new SymmetricSecurityKey
                                (Encoding.UTF8.GetBytes(_config["JWT:secret"]));
            SigningCredentials signcred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: signcred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
