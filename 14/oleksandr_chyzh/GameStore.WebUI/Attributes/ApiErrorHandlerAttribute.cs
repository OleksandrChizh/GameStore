using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.WebUI.Attributes
{
    public class ApiErrorHandlerAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(
            HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception != null && 
                actionExecutedContext.Exception.GetType() == typeof(EntityNotFoundException))
            {
                string message = (actionExecutedContext.Exception as EntityNotFoundException).EntityType.Name;
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }

            return Task.FromResult<object>(null);
        }
    }
}