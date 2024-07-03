using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DATA;
using WebApplication1.ViewMoDel;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class EmpController : Controller
    {
        // GET: Admin/Emp
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Login1(LoginVM login)
        {
            using (var db = new Entities())
            {
                Session["User"] = login.Username;
                var a = Session["User"];
                DangNhap dn = db.DangNhaps.FirstOrDefault(x => x.Username == login.Username);
                if (dn != null )
                {
                    if ( login.Password == dn.Password)
                    {
                        return RedirectToAction("Index", "Product", new { Area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Login", "Emp", new { Area = "Admin" });
                    }

                }
                else
                {
                    return RedirectToAction("Login","Emp", new { Area = "Admin" });
                }
                
            }
        }
    }
}