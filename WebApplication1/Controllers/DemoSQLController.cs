using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DATA;

namespace WebApplication1.Controllers
{
    public class DemoSQLController : Controller
    {
        // GET: DemoSQL
        List<Category> listCategory = null;
        public ActionResult DemoSQL()
        {

            using (var context = new Entities())
            {
                listCategory = context.Categories.ToList();
                //listCategory = (from e in context.Categories select e).ToList();
            }
            return View(listCategory);
        }

        public ActionResult Detail(string id)
        {
            Category model;
            try
            {
                int idcheck = int.Parse(id);
                using (Entities db = new Entities())
                {
                    model = db.Categories.FirstOrDefault(x => x.IdCategory == idcheck);
                    model = (from s in db.Categories
                             where s.IdCategory == idcheck
                             select s).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return Content("Có lỗi!");
            }
            return View(model);
        }


        #region "Save"
        [HttpPost]
        public ActionResult Save(Category modelinput)
        {
            Category model;
            using (Entities db = new Entities())
            {
                model = db.Categories.FirstOrDefault(x => x.IdCategory == modelinput.IdCategory);
                if (model == null)
                {
                    ViewBag.ErrorMess = "Không tồn tại ãm này";
                    return View("Detail", modelinput);
                }
                model.NameCategory = modelinput.NameCategory;
                db.SaveChanges();

                //var listCategory = db.Categories.ToList();
                //return View("Index",listCategory);
                return RedirectToAction("DemoSQL");
            }


        }
        #endregion

        #region "Delete"
        public ActionResult Delete(string id)
        {
            Category model;
            int idcheck = int.Parse(id);
            using (Entities db = new Entities())
            {
                model = db.Categories.FirstOrDefault(x => x.IdCategory == idcheck);
                db.Categories.Remove(model);
                db.SaveChanges();

                //var listCategory = db.Categories.ToList();
                //return View("Index",listCategory);
                return RedirectToAction("DemoSQL");
            }


        }
        #endregion

        public ActionResult AddView()
        {
            return View();
        }

            #region "Add"
            [HttpPost]
        public ActionResult Add(Category modelinput)
        {

                Category model;
                using (Entities db = new Entities())
                {
                    model = db.Categories.FirstOrDefault(x => x.IdCategory == modelinput.IdCategory);
                    if (model != null)
                    {
                        ViewBag.ErrorMess = "Mã đã tồn tại";
                        return View("AddView", modelinput);

                    }
                db.Categories.Add(modelinput);
                    db.SaveChanges();

                    //var listCategory = db.Categories.ToList();
                    //return View("Index",listCategory);
                    return RedirectToAction("DemoSQL");
                }
            


        }
        #endregion

    }

}