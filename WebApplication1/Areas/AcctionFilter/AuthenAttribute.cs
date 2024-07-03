using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Areas.Admin.Controllers;

namespace WebApplication1.Areas.AcctionFilter
{
    public class AuthenAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var a = filterContext.HttpContext.Session["User"];
            EmpController emp = filterContext.Controller as EmpController;
            if(a == null )
            {
                var url = new RouteValueDictionary(
                    new
                    {
                        action = "Login", controller = "Emp", area = "Admin"
                    });
                filterContext.Result = new RedirectToRouteResult(url);
            }


            base.OnActionExecuted(filterContext);
        }
    }
}