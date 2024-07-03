using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DATA;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var db = new Entities())
            {
                ViewBag.ListCate = db.Categories.ToList();

            }
            base.OnActionExecuting(filterContext);
        }
    }
}