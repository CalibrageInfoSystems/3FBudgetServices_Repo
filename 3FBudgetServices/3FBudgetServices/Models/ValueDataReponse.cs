using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3FBudgetServices.Models
{
    public class ValueDataReponse<T>:DataResponse
    {
        public T Result { get; set; }
    }
}