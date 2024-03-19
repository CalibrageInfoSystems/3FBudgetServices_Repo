using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3FBudgetServices.Models
{
    public class DataResponse
    {
        public bool IsSuccess { get; set; }
        public int AffectedRecords { get; set; }
        public string EndUserMessage { get; set; }
        public DataResponse()
        {
            IsSuccess = false;
            AffectedRecords = 0;

        }
    }
}