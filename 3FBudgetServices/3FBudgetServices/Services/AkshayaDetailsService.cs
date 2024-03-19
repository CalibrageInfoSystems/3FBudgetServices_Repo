using _3FBudgetServices.Models;
using Akshaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace _3FBudgetServices.Services
{
    public class AkshayaDetailsService
    {
        public AkshayaEntities akshaya_Entities = new AkshayaEntities();

        public ListDataResponse<GetAllRoles_Result> GetAllRoles(int? Id)
        {
            ListDataResponse<GetAllRoles_Result> response = new ListDataResponse<GetAllRoles_Result>();

            try
            {

                var res = akshaya_Entities.GetAllRoles(Id).ToList();
                if (res.Count() > 0)
                {
                    response.ListResult = res;
                    response.AffectedRecords = response.ListResult.Count();
                    response.IsSuccess = true;
                    response.EndUserMessage = "Get All Roles Details Successfully";
                }
                else
                {
                    response.AffectedRecords = 0;
                    response.IsSuccess = true;
                    response.EndUserMessage = "No Roles Found";
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