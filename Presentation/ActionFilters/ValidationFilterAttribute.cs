using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"]; // context içerisinde hangi controllerda olduğumuz bilgisi. Book, outdoor, custommer
            var action = context.RouteData.Values["action"]; // action için context üzerinden action ile hangi metodun çalıştığını öğrenmiş olacağız

            //Dto bilgisi
            var param = context.ActionArguments
                .SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value;

            if (param is null)
            {
                context.Result = new BadRequestObjectResult($"Object is null."
                    + $"Controller : {controller}" + $"Action: {action}");
                return; // 400
            }
            if (!context.ModelState.IsValid) // Eğer geçersiz bir istekse. metodun içeriside olan valdation işlemi
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
