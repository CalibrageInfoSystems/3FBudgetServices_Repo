using _3FBudgetServices.Models;
using _3FBudgetServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace _3FBudgetServices.Controllers
{
    public class UsersController : ApiController
    {
        public UserDetailsService userinfo = new UserDetailsService();

        [CustomAuthorizationAttribute]
        [HttpGet]
        [Route("GetUsers")]

        public IHttpActionResult GetUsers()
        {
            var result = userinfo.GetUserDetails();

            return Ok(result);
        }
    }
}
