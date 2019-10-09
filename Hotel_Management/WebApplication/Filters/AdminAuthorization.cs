using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DomainObjects.Enums;

namespace WebApplication.Filters
{
    public class AdminAuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
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
            }
            else
            {
                var currentUser = (User)HttpContext.Current.Session["CurrentUser"];
                if(currentUser.RoleId != RolesEnum.Admin)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
        }
    }
}