using _3FBudgetServices.Models;
using _3FBudgetServices.Services;
using Akshaya;
using SmartPalm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _3FBudgetServices.Controllers
{
    [RoutePrefix("api/SAPDetails")]
    public class SAPDetailsController : ApiController
    {
        //SmartPalmEntities
        public SmartPalmEntities smartPalmEntities = new SmartPalmEntities();
        //AkshayaEntities
        public AkshayaEntities akshaya_Entities = new AkshayaEntities();
        //SAPDetailsService
        public SAPDetailsService sapdata = new SAPDetailsService();
        private object result;

        [CustomAuthorizationAttribute]
        [HttpGet]
        [Route("GetMatrixInfo/{Code}/{Date}")]
        public IHttpActionResult GetInventoryData(string Code, DateTime Date)
        {
            try
            {
                // Get the current server date
                DateTime currentDate = DateTime.Now.Date;

                // Get the current server time
                TimeSpan currentTime = DateTime.Now.TimeOfDay;

                // Format the current date
                string formattedDate = currentDate.ToString("yyyy-MM-dd");

                // Format the current time
                string formattedTime = currentTime.ToString(@"hh\:mm\:ss\.fff");

                // Concatenate the date and time
                string concatenatedDateTime = formattedDate + " " + formattedTime;
                // Convert concatenatedDateTime string to DateTime
                DateTime concatenatedDateTimeParsed = DateTime.ParseExact(concatenatedDateTime, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                // Check if the entry already exists in the database
                var existingEntry = smartPalmEntities.BudgetSheet_3F
                    .FirstOrDefault(entry => entry.MatrixCode == Code || entry.InputDate == concatenatedDateTimeParsed);
                //existingEntry
                if (existingEntry != null)
                {
                    return BadRequest("You have already accessed this method for the provided code and date.");
                }
                
                //if not existing Entry
                else
                {
                    string SapData=ConfigurationManager.AppSettings["SAPCode"];

                    //dynamic result;
                    if (Code == SapData)
                    {
                        // Fetch WhsCode and ItemCode from somewhere, or provide default values if necessary
                        string defaultWhsCode = "AgrGAPYG";
                        string defaultItemCode = "H01";

                        // Proceed with executing the method
                        result = sapdata.GetInventoryData(defaultWhsCode, defaultItemCode);
                    }
                    else if (Code == ConfigurationManager.AppSettings["Code"])
                    {
                        result = smartPalmEntities.GetUserDetails(null).ToList();
                    }
                    else if(Code == ConfigurationManager.AppSettings["Code"])
                    {
                        int Id = 1;
                        result= akshaya_Entities.GetAllRoles(Id).ToList();
                    }


                    // Save the method name into the table
                    var newEntry = new BudgetSheet_3F
                    {
                        MatrixCode = Code,
                        InputDate = Convert.ToDateTime(concatenatedDateTime),
                        CreatedDate = DateTime.Now
                        // MethodName = "GetInventoryData"
                    };

                    smartPalmEntities.BudgetSheet_3F.Add(newEntry);
                    smartPalmEntities.SaveChanges();
                }
               

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Log.Error(ex);

                // You can provide custom messages based on the type of exception
                if (ex is DbEntityValidationException)
                {
                    return BadRequest("Validation error occurred while saving data to the database.");
                }
                else if (ex is DbUpdateException)
                {
                    return BadRequest("Error occurred while updating the database.");
                }
                //else
                //{
                //    // For any other type of exception, provide a generic message
                //    return InternalServerError("An error occurred while processing your request.");
                //}
            }
            return Ok();
        }

        //public IHttpActionResult GetInventoryData(string WhsCode, string ItemCode)
        //{
        //var result = sapdata.GetInventoryData(WhsCode, ItemCode);

        //return Ok(result);
        //}
    }
}
