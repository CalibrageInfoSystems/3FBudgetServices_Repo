using _3FBudgetServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _3FBudgetServices.Controllers
{
    public class AuthenticationController : ApiController
    {
        public AuthService authService = new AuthService();
        [HttpGet]
        [Route("GenerateToken")]
        public IHttpActionResult GenerateToken()
        {
            var res = authService.GenerateToken();
            return Ok(res);
        }

        [HttpGet]
        [Route("RefreshToken")]
        public IHttpActionResult RefreshToken(string Token)
        {
            var refreshToken = authService.RefreshToken(Token);
            return Ok(refreshToken);
        }
    }
}
