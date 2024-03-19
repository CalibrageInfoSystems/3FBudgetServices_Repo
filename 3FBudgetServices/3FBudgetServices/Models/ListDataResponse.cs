using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3FBudgetServices.Models
{
    public class ListDataResponse<T>:DataResponse
    {
        public IEnumerable<T> ListResult { get; set; }
    }
}