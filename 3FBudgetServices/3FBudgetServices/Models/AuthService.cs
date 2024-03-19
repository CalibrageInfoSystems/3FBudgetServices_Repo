using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;

namespace _3FBudgetServices.Models
{
    public class AuthService
    {
        public string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string key = ConfigurationManager.AppSettings["randomkey"].ToString();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            var accessTokenDescriptor = new SecurityTokenDescriptor
            {

                Expires = DateTime.Now.AddMinutes(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);
            var accessTokenString = tokenHandler.WriteToken(accessToken);
            return accessTokenString;
        }

        public string RefreshToken(string Token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            byte[] keyBytes = Encoding.UTF8.GetBytes(Token);
            var refreshTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);

            var refreshTokenString = tokenHandler.WriteToken(refreshToken);
            return refreshTokenString;
        }
    }
}