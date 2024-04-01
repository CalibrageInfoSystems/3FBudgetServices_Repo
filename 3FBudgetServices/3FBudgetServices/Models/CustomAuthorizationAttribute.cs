using SmartPalm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace _3FBudgetServices.Models
{
    
    public class CustomAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string token = GetTokenFromHeaders(actionContext);
            //string apiKey = GetApiKeyFromHeaders(actionContext);
            // Perform your custom authorization logic using the token and other parameters
            //bool isAuthorized = CheckAuthorizationLogic(token, apiKey);
            bool checktoken = ischecktoken(token);
            
            bool expiredtoken = IsTokenExpired(token);
            if(checktoken == false)
            {
                string errorMessage = "Token Is inactive";

                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent($"{{ \"error\": \"{errorMessage}\" }}", Encoding.UTF8, "application/json")
                };

                actionContext.Response = response;
            }
            if (expiredtoken == false)
            {
                //actionContext.Response = "";
                string errorMessage = "Token Is Expired";

                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent($"{{ \"error\": \"{errorMessage}\" }}", Encoding.UTF8, "application/json")
                };

                actionContext.Response = response;
            }
            else
            {
                bool isAuthorized = CheckAuthorizationLogic(token);

                if (!isAuthorized)
                {
                    string errorMessage = "invalid token";

                    var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent($"{{ \"error\": \"{errorMessage}\" }}", Encoding.UTF8, "application/json")
                    };

                    actionContext.Response = response;
                }
            }
        }
        private string GetTokenFromHeaders(HttpActionContext actionContext)
        {
            IEnumerable<string> headerValues;
            //string token = this.Request.Headers["Authorization"].ToString(); 
            if (actionContext.Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                string header = headerValues.FirstOrDefault();
                if (!string.IsNullOrEmpty(header))
                {
                    return header;
                }
            }

            return null;
        }


        //private readonly string secretKey;
        private bool ischecktoken(string token)
        {
            using (var context = new SmartPalmEntities())
            {
                var accessToken = context.Accesstokens.FirstOrDefault(a => a.Token == token);
                bool checktoken1;
                if (accessToken.IsActive == false)
                {
                    return checktoken1 = false;
                }
                else
                {
                    return checktoken1 = true;
                }
                bool checktoken = checktoken1;
                return checktoken;
            }
            
        }

        private bool CheckAuthorizationLogic(string token)
        {

            string validate = ConfigurationManager.AppSettings["randomkey"];
            string jwt = token;


            string[] jwtParts = jwt.Split('.');
            string payloadBase64 = jwtParts.Length > 1 ? jwtParts[1] : string.Empty;




            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(validate);
            byte[] signatureBytes = Base64UrlDecode(jwtParts[2]);


            string headerPayload = $"{jwtParts[0]}.{payloadBase64}";
            byte[] headerPayloadBytes = Encoding.UTF8.GetBytes(headerPayload);


            var hmac = new HMACSHA256(secretKeyBytes);

            byte[] computedHash = hmac.ComputeHash(headerPayloadBytes);
            bool isValidToken1;

            if (AreEqual(computedHash, signatureBytes))
            {
                return isValidToken1 = true;
            }
            else
            {
                return isValidToken1 = false;
            }


            bool isAuthorized = isValidToken1;

            return isAuthorized;


        }
        static byte[] Base64UrlDecode(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/").Replace("-", "+");
            return Convert.FromBase64String(base64);
        }
        static bool AreEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null || a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }


        public bool IsTokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (securityToken == null)
                    return true; // Token is invalid or malformed

                foreach (var claim in securityToken.Claims)
                {
                    if (claim.Type == "token_type" && claim.Value == "refresh")
                    {
                        return true;
                    }
                }
                // Get the expiration time of the token
                var expirationTime = securityToken.ValidTo;

                // Check if the token expiration time is in the past
                var isExpired = expirationTime >= DateTime.UtcNow;

                return isExpired;
            }
            catch (Exception)
            {
                return true; // Token is invalid or malformed
            }
        }

    
    }
}