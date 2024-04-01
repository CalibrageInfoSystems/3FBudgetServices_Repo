using Microsoft.IdentityModel.Tokens;
using SmartPalm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;


namespace _3FBudgetServices.Models
{
    
    public class AuthService
    {
        //private SmartPalmEntities _Context = new SmartPalmEntities();
        public string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string key = ConfigurationManager.AppSettings["randomkey"].ToString();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            var accessTokenDescriptor = new SecurityTokenDescriptor
            {

                Expires = DateTime.Now.AddMinutes(5),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);
            var accessTokenString = tokenHandler.WriteToken(accessToken);

            SaveTokenToDatabase(accessTokenString,isactive:true);
            return accessTokenString;
           
        }



       
        public void SaveTokenToDatabase(string token, bool isactive)
    {
        using (var context = new SmartPalmEntities())
        {
            var accessToken = new Accesstoken
            {
                Token = token,
                IsActive = isactive
            };
                context.Accesstokens.Add(accessToken);
                context.SaveChanges();

        }
            

    }

    public string RefreshToken(string token)
        {
            using (var context = new SmartPalmEntities())
            {
                var accessToken = context.Accesstokens.FirstOrDefault(a => a.Token == token);
                if (accessToken != null)
                {
                    accessToken.IsActive = false;
                    context.SaveChanges(); 
                }
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["randomkey"]);

            var refreshTokenDescriptor = new SecurityTokenDescriptor
            {

                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
               
            };

            var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);
            var refreshTokenString = tokenHandler.WriteToken(refreshToken);
            SaveTokenToDatabase(refreshTokenString, isactive: true);
            return refreshTokenString;

        }

        
    }
   
}