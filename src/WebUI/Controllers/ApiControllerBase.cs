using Tawala.WebUI.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Linq;

namespace Tawala.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();


        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    var x = context.ActionDescriptor.Parameters;
        //    context.Result = new Response()
        //    {
        //        code = StatusCodes.Status500InternalServerError,
        //        Data = context.Result
        //    };

        //    base.OnActionExecuted(context);

        //}

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var x = context.ActionDescriptor.RouteValues;
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                List<ModelErrorCollection> ee = new List<ModelErrorCollection>();
                foreach (var item in modelState)
                {
                    ee.Add(item.Value.Errors);
                }
                context.Result = new ObjectResult(ee)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            base.OnActionExecuting(context);
        }



    }
    public class CustomAuthAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        //serve roles

        public CustomAuthAttribute(params string[] args)
        {
            Args = args;
        }

        public string[] Args { get; }

        public   void OnAuthorization(AuthorizationFilterContext context)
        {
            //Custom code ...

            var RouteValues = context.ActionDescriptor.RouteValues;

            //controller name
            //action name
            //---------------
            //Roles 1,2,3

            //الاذونات 
            //List <Roles >
            //id 
            //action name
            //controller name

            //Resolving a custom Services from the container
         //   var service = context.HttpContext.RequestServices.GetRequiredService<ISample>();
            //string name = service.GetName(); // returns "anish"

            //Return based on logic
            context.Result = new UnauthorizedResult();
            
        }

    }
    //public class Sample : ISample
    //{
    //    public string GetName() => "anish";
    //}
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext authorizationFilterContext)
        {
            var policyProvider = authorizationFilterContext.HttpContext
                .RequestServices.GetService<IAuthorizationPolicyProvider>();
            var policy = await policyProvider.GetPolicyAsync("Read");
            var requirement = (ClaimsAuthorizationRequirement)policy.Requirements
                .First(r => r.GetType() == typeof(ClaimsAuthorizationRequirement));

            if (authorizationFilterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!authorizationFilterContext.HttpContext
                  .User.HasClaim(x => x.Value == requirement.ClaimType))
                {
                    authorizationFilterContext.Result =
                       new ObjectResult(new UnauthorizedResult(

                           ));
                }
            }
        }
    }

}
