using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.AcctionFilter;
using WebApplication1.DATA;
using WebApplication1.ViewMoDel;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Authen]
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            
            List<ProductVM> listPro = new List<ProductVM>();
            using (var db = new Entities())
            {
                var a = Session["User"];
                if (a != null)
                {
                    listPro = db.Products.Join(db.Categories,
                        p => p.IdCategory,
                        c => c.IdCategory,
                        (p, c) =>
                        new ProductVM
                        {
                            IdProduct = p.IdProduct,
                            NameProduct = p.NameProduct,
                            Price = p.Price,
                            IdCategory = p.IdCategory,
                            NameCategory = c.NameCategory
                        }).ToList();
                    return View(listPro);
                }
                else
                {
                    return RedirectToAction("Login", "Emp", new { Area = "Admin" });
                }
            }
        }
        public ActionResult Add(int? idpro)
        {
            ProductVM product = null;

            using (var db = new Entities())
            {
                if (idpro != null)
                {
                    product = db.Products.Where(x => x.IdProduct == idpro)
                    .Select(x => new ProductVM
                    {
                        IdProduct = x.IdProduct,
                        NameProduct = x.NameProduct,
                        IdCategory = x.IdCategory,
                        Price = x.Price
                    }).FirstOrDefault();
                }

               
            }
            if (product != null)
            {
                return View(product);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Save(ProductVM proVM)
        {
            string pathName = Path.Combine(Server.MapPath("~/Imgs/Products/"), proVM.IdCategory + ".png");
            proVM.FilePro.SaveAs(pathName);


            using (var db = new Entities())
            {
                Product pr = new Product();
                pr = db.Products.FirstOrDefault(x => x.IdCategory == proVM.IdCategory);
                ViewBag.ListCate = db.Categories.ToList();
                if (proVM.IdProduct == null)
                {
                    if (ModelState.IsValid)
                    {
                        Product pr = new Product();
                        // pr.IdProduct = proVM.IdProduct;
                        pr.NameProduct = proVM.NameProduct;
                        pr.IdCategory = proVM.IdCategory;
                        pr.Price = proVM.Price;
                        db.Products.Add(pr);
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    Product prod = db.Products.FirstOrDefault(x => x.IdProduct == proVM.IdProduct);


                    prod.NameProduct = proVM.NameProduct;
                    prod.IdCategory = proVM.IdCategory;
                    prod.Price = proVM.Price;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }


        }

        public ActionResult Xoa(int idpro)
        {
            Product model;
            int idcheck = idpro;
            using (Entities db = new Entities())
            {
                model = db.Products.FirstOrDefault(x => x.IdProduct == idcheck);
                db.Products.Remove(model);
                db.SaveChanges();

                //var listCategory = db.Categories.ToList();
                //return View("Index",listCategory);
                return RedirectToAction("Index");
            }
        }

        public ActionResult List()
        {
            using (Entities db = new Entities())
            {
                var listcate = db.Categories.ToList();
                listcate.Insert(0, new Category { IdCategory = -5, NameCategory = "tất cả" });
                ViewBag.Listcate = listcate;
                return View();
            }
        }
        public ActionResult search(int idCate)
        {
            int reCountPro = 0;
            using (var db = new Entities())
            {
                if (idCate == -5)
                {
                    reCountPro = db.Products.Count();

                }
                else
                {
                    reCountPro = db.Products.Where(x => x.IdCategory == idCate).Count();
                }
            }
            return Content("Kết quả  " + reCountPro);
        }
        public ActionResult search1(int idCate)
        {
            if (idCate == -5)
            {
                List<ProductVM> listPro = new List<ProductVM>();
                using (var db = new Entities())
                {
                    listPro = db.Products.Join(db.Categories,
                        p => p.IdCategory,
                        c => c.IdCategory,
                        (p, c) =>
                        new ProductVM
                        {
                            IdProduct = p.IdProduct,
                            NameProduct = p.NameProduct,
                            Price = p.Price,
                            IdCategory = p.IdCategory,
                            NameCategory = c.NameCategory
                        }).ToList();

                }
                
                return PartialView(listPro);
            }
            else
            {
                List<ProductVM> listPro = new List<ProductVM>();
                using (var db = new Entities())
                {
                    listPro = db.Products.Join(db.Categories,
                        p => p.IdCategory,
                        c => c.IdCategory,
                        (p, c) =>
                        new ProductVM
                        {
                            IdProduct = p.IdProduct,
                            NameProduct = p.NameProduct,
                            Price = p.Price,
                            IdCategory = p.IdCategory,
                            NameCategory = c.NameCategory
                        }).Where(x => x.IdCategory == idCate).ToList();

                }
                return PartialView(listPro);
            }

            
        }

        public ActionResult search2(int idCate)
        {
            int countProInPage = 5;
            List<ProductVM> ListPro = null;
            if (idCate == -5)
            {
                using (var db = new Entities())
                {
                    ListPro = db.Products.Join(db.Categories,
                        p => p.IdCategory,
                        c => c.IdCategory,
                        (p, c) =>
                        new ProductVM
                        {
                            IdProduct = p.IdProduct,
                            NameProduct = p.NameProduct,
                            Price = p.Price,
                            IdCategory = p.IdCategory,
                            NameCategory = c.NameCategory
                        }).ToList();

                    var co = ListPro.Count();
                    ViewBag.CountPages = co % countProInPage == 0 ? co / countProInPage : co / countProInPage + 1;
                }
            }
            else
            {
                using (var db = new Entities())
                {
                    ListPro = db.Products.Join(db.Categories,
                        p => p.IdCategory,
                        c => c.IdCategory,
                        (p, c) =>
                        new ProductVM
                        {
                            IdProduct = p.IdProduct,
                            NameProduct = p.NameProduct,
                            Price = p.Price,
                            IdCategory = p.IdCategory,
                            NameCategory = c.NameCategory
                        }).Where(x => x.IdCategory == idCate)
                        .ToList();

                }
                
                
            }
            ViewBag.CountPages = Math.Round(((double)(ListPro.Count() / countProInPage)) + 0, 5);
            return PartialView(ListPro);

        }
    }
}