using Entities.LogModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private readonly ILoggerService _logger;

        public LogFilterAttribute(ILoggerService logger)
        {
            _logger = logger;
        }

        // Metodun Öncesinde bir log alacağız. (OnActionExecuted -> metodun sonrasında)
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // loglamayla ilgili kurgu burada
            _logger.LogInf(Log("OnActionExecuting", context.RouteData));  // Json Dosyası elimizde olacak
            // "OnActionExecuting", context.RouteData -> Model adı, Log un kendisi
        }

        private string Log(string modelName, Microsoft.AspNetCore.Routing.RouteData routeData)
        {
            var logDetails = new LogDetails()
            {
                ModelName = modelName,
                Controller = routeData.Values["controller"],
                Action = routeData.Values["Action"],
            };

            // Her ifadede id değeri yok. Id değerini okuyacaksam parametre sayısına bakmalıyız.
            // Eğer values 3 ya da daha fazlaysa id değeri vardır
            if (routeData.Values.Count >= 3)
                logDetails.Id = routeData.Values["Id"];

            return logDetails.ToString(); // Serilaze

        }
    }
}