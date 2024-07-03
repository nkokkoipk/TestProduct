using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.AcctionFilter;
using WebApplication1.DATA;
using WebApplication1.ViewMoDel;

namespace WebApplication1.Controllers
{ 
////{
////    [AuthenSel]
    public class ProductController : Controller
    {
        // GET: Product
        //[Authen]
        
        public ActionResult Index()
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
            return View(listPro);
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

                ViewBag.ListCate = db.Categories.ToList();
               
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
            //string pathName = Path.Combine(Server.MapPath("~/Imgs/Products/"),proVM.IdCategory+".png");
            //proVM.FilePro.SaveAs(pathName);


            using (var db = new Entities())
            {
                //Product pr = new Product();
                //pr = db.Products.FirstOrDefault(x => x.IdCategory == proVM.IdCategory);
                //ViewBag.ListCate = db.Categories.ToList();
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


            return Content("");
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

        public ActionResult search2(int? idCate, int? intpage)
        {
            int pageSize = 5;
            int page = 1;
            int countPage = 0;

            List<ProductVM> listPro = null;
            if (idCate == -5)
            {
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
            }
            else
            {
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
                        }).Where(x => x.IdCategory == idCate)
                        .ToList();

                }
                
                
            }
            countPage = (listPro.Count() % pageSize == 0) ? (listPro.Count() / pageSize) :
                                        ((listPro.Count() / pageSize) + 1);

            if (intpage == null)
            {
                listPro = listPro.Take(pageSize).ToList();
            }
            else
            {
                // 11
                // page = 1 => skip (page -1) *5 =  => take 5
                // page =2 => skipp 5 =(page -1) * 5=> take 5
                // Page =3 => skip 10  = (page -1)* 5=> take 5
                // Page =4 => skip 15 =(page -1)* 5 => take 5
                int startIndex = (intpage.Value - 1) * pageSize;
                listPro = listPro.Skip(startIndex).Take(pageSize).ToList();
            }

            ViewBag.CountPages = countPage;
            return PartialView(listPro);

        }
    }
}