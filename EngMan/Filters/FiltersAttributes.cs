using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EngMan.Filters
{
    public class NotFoundExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null)
            {
                throw new Exception("Action executed context is null");
            }
            actionExecutedContext.Response =  actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, 
                actionExecutedContext.Exception.Message);
        }
    }
    
    public class CheckParametersForNullAttribute : ActionFilterAttribute
    {
        private readonly Func<Dictionary<string, object>, bool> _validate;

        public CheckParametersForNullAttribute() : this(arguments => arguments.ContainsValue(null))
        { }

        public CheckParametersForNullAttribute(Func<Dictionary<string, object>, bool> checkCondition)
        {
            _validate = checkCondition;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (_validate(actionContext.ActionArguments))
            {
                if (actionContext == null)
                {
                    throw new Exception("Action context is null");
                }
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "The argument cannot be null");
            }
        }
    }

    public class CheckResultForNullAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null)
            {
                throw new Exception("Action executed context is null");
            }
            else if (actionExecutedContext.Exception != null)
            {
                return;
            }
            actionExecutedContext.Response.TryGetContentValue(out object objValue);
            if (objValue == null)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found 404");
            }
        }
    }
}