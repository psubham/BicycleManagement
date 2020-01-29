using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.UserException
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(DuplicateElement))
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            else if (exceptionType == typeof(BicycleTypeException))
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            else if (exceptionType == typeof(BicyclesException))
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            else 
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            


        }
    }
}
