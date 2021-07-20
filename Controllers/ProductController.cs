using DAMSWeb.Models;
using DAMSWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAMSWeb.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Product_categoryEntitiesDB _db = new Product_categoryEntitiesDB();
        public ActionResult Index()
        {
            List<ProductViewModel> pvm = new List<ProductViewModel>();

            var products = _db.tblProducts.ToList();
            foreach (var item in products)
            {
                pvm.Add(new ProductViewModel() { ProductId = item.ProductId, CategoryName = item.tblCategory.CategoryName, UnitPrice = item.UnitPrice, SellingPrice = item.SellingPrice, Photo = item.Photo });
            }
            return View(pvm);
        }
     
        public ActionResult Create()
        {
            ViewBag.Categories = _db.tblCategories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductViewModel pvm)
        {
            tblProduct tb = new tblProduct();
            tb.CategoryId = pvm.CategoryId;
            tb.ProductName = pvm.ProductName;
            tb.UnitPrice = pvm.UnitPrice;
            tb.SellingPrice = pvm.SellingPrice;
            HttpPostedFileBase fup = Request.Files["Photo"];
            if (fup != null)
            {

                tb.Photo = fup.FileName;
                fup.SaveAs(Server.MapPath("/ProductImages/"+fup.FileName));
            }
            _db.tblProducts.Add(tb);
            _db.SaveChanges();
             ViewBag.Message="Product Created";
            ViewBag.Categories = _db.tblCategories.ToList();
            return View();
        }
        public ActionResult Edit(int id)
        {
            var products = _db.tblProducts.Where(p=>p.ProductId==id).FirstOrDefault();
            ProductViewModel pvm = new ProductViewModel();
            pvm.ProductId = products.ProductId;
            pvm.ProductName = products.ProductName;
            pvm.CategoryId = products.CategoryId;
            pvm.UnitPrice = products.UnitPrice;
            pvm.SellingPrice = products.SellingPrice;
            pvm.Photo = products.Photo;

          

            ViewBag.Categories = _db.tblCategories.ToList();
            return View(pvm);
        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel pvmm)
        {
            var products = _db.tblProducts.Where(p => p.ProductId == pvmm.ProductId).FirstOrDefault();
            ProductViewModel pvm = new ProductViewModel();
            products.ProductName = pvmm.ProductName;
            products.CategoryId = pvmm.CategoryId;
            products.UnitPrice = pvmm.UnitPrice;
            products.SellingPrice = pvmm.SellingPrice;
            HttpPostedFileBase fup = Request.Files["Photo"];
            if (fup != null)
            {
                if (fup.FileName != "")
                {
                    System.IO.File.Delete(Server.MapPath("/ProductImages/" + pvmm.Photo));
                    products.Photo = fup.FileName;
                    fup.SaveAs(Server.MapPath("/ProductImages/" + fup.FileName));
                }
            }
            _db.SaveChanges();

            ViewBag.Message = "Product Created";



            ViewBag.Categories = _db.tblCategories.ToList();
            return RedirectToAction("index");
        }
    }
}