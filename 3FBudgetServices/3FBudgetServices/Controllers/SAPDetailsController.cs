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
    [RoutePrefix("api/SAPDetails")]
    public class SAPDetailsController : ApiController
    {
        public SAPDetailsService sapdata = new SAPDetailsService();

        [CustomAuthorizationAttribute]
        [HttpGet]
        [Route("GetInventoryData/{WHSCode}/{ItemCode}")]

        public IHttpActionResult GetInventoryData(string WhsCode, string ItemCode)
        {
            var result = sapdata.GetInventoryData(WhsCode, ItemCode);

            return Ok(result);
        }
    }
}
