using _3FBudgetServices.Models;
using SmartPalm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Users
{
   public class UserDetails
    {
        public SmartPalmEntities smartPalmEntities = new SmartPalmEntities();
        public ListDataResponse<GetUserDetails_Result> GetUserDetails()
        {
            ListDataResponse<GetUserDetails_Result> response = new ListDataResponse<GetUserDetails_Result>();

            try
            {
                
                var res = smartPalmEntities.GetUserDetails(null).ToList();
                if (res.Count() > 0)
                {
                    response.ListResult = res;
                    response.AffectedRecords = response.ListResult.Count();
                    response.IsSuccess = true;
                    response.EndUserMessage = "Get Farmer Details Successful";
                }
                else
                {
                    response.AffectedRecords = 0;
                    response.IsSuccess = true;
                    response.EndUserMessage = "No Farmer Found";
                }
            }
            catch(Exception ex)
            {
                response.AffectedRecords = 0;
                response.IsSuccess = true;
                response.EndUserMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
              
            }
            return response;
        }
    }
}
