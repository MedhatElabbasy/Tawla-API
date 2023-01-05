using Tawala.Application.Common;
using Tawala.Application.Common.Utility;
using Tawala.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.WebUI.Filters
{
    public class ApiGeneralFilter : ActionFilterAttribute
    {
        private readonly IConfiguration configuration;

        public ApiGeneralFilter(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var lang = context.HttpContext.Request.Headers.FirstOrDefault(a => a.Key.ToLower() == "lang").Value.ToString();
            var source = context.HttpContext.Request.Headers.FirstOrDefault(a => a.Key.ToLower() == "source").Value.ToString();
            GlobalSetting.ImageUrl = configuration.GetSection("PathSetting:UrlPath").Value;
            switch (lang)
            {
                case "en":
                    
                    RequestUtility.IsArabic = false;
                    RequestUtility.Language = UserLanguage.English;
                    break;
                case "ar":
                    RequestUtility.IsArabic = true;                    
                    RequestUtility.Language = UserLanguage.Arabic;
                    break;
                default:
                    RequestUtility.IsArabic = true;
                    RequestUtility.Language = UserLanguage.Arabic;
                    break;
            }
            switch (lang)
            {
                case "1":
                    RequestUtility.Source = Source.Mobile;
                    break;
                case "2":
                    RequestUtility.Source = Source.Web;
                    break;
                default:
                    RequestUtility.Source = Source.Web;
                    break;
            }

            base.OnActionExecuting(context);
        }

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    //context.Result = new ObjectResult(context.Result)
        //    //{

        //    //    StatusCode = StatusCodes.Status500InternalServerError
        //    //};

        //    base.OnActionExecuted(context);
        //}


    }
}
