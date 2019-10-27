using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Filters
{
    public class GeneralAuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {

                return;
            }


            if (HttpContext.Current.Session["CurrentUser"] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                filterContext.Controller.TempData["message"] = "You must be logged to browse this page";
            }
        }
    }
}