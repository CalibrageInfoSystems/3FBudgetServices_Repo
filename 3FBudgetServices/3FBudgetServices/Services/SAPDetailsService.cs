using _3FBudgetServices.Models;
using Sap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3FBudgetServices.Services
{
    public class SAPDetailsService
    {
        public Test_liveEntities test_LiveEntities = new Test_liveEntities();

        public ListDataResponse<GetInventoryData_Result> GetInventoryData(string WhsCode, string ItemCode)
        {
            ListDataResponse<GetInventoryData_Result> response = new ListDataResponse<GetInventoryData_Result>();

            try
            {

                var res = test_LiveEntities.GetInventoryData(WhsCode, ItemCode).ToList();
                if (res.Count() > 0)
                {
                    response.ListResult = res;
                    response.AffectedRecords = response.ListResult.Count();
                    response.IsSuccess = true;
                    response.EndUserMessage = "Get Inventory Details Successfully";
                }
                else
                {
                    response.AffectedRecords = 0;
                    response.IsSuccess = true;
                    response.EndUserMessage = "No Inventory Found";
                }
            }
            catch (Exception ex)
            {
                response.AffectedRecords = 0;
                response.IsSuccess = true;
                response.EndUserMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

            }
            return response;
        }
    }
}