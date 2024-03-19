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
    [RoutePrefix("api/AkshayaDetails")]
    public class AkshayaDetailsController : ApiController
    {
        public AkshayaDetailsService sapdata = new AkshayaDetailsService();

        [CustomAuthorizationAttribute]
        [HttpGet]
        [Route("GetAllRoles/{Id}")]

        public IHttpActionResult GetAllRoles(int? Id)
        {
            var result = sapdata.GetAllRoles(Id);

            return Ok(result);
        }
    }
}
