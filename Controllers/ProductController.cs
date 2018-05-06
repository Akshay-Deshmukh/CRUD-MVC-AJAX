using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD.Models;
using Newtonsoft.Json;

namespace CRUD.Controllers
{
    public class ProductController : Controller
    {
         SalesContext dbContext;

        public ProductController()
        {
            dbContext = new SalesContext();
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProductList()
        {
            List<ViewModel.ProductViewModel> productList = dbContext.Products.Select(x => new ViewModel.ProductViewModel
            {
                Id = x.ProductId,
                Name = x.Name,
                Price = x.price
            }).ToList();

            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductById(int productId)
        {
            var product = dbContext.Products.FirstOrDefault(x => x.ProductId == productId);

            string value = string.Empty;
            value = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveData(ViewModel.ProductViewModel model)
        {
            var result = false;
            try
            {
                if (model.Id > 0)
                {
                    var product = dbContext.Products.FirstOrDefault(x => x.ProductId == model.Id);
                    product.Name = model.Name;
                    product.price = model.Price;
                    dbContext.SaveChanges();
                    result = true;
                }
                else
                {
                    Product product = new Product();
                    product.Name = model.Name;
                    product.price = model.Price;
                    dbContext.Products.Add(product);
                    dbContext.SaveChanges();
                    result = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProduct(int productId)
        {
            bool result = false;
            var product = dbContext.Products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
