using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD.Models;
using Newtonsoft.Json;

namespace CRUD.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        SalesContext dbContext;
        public SalesController()
        {
            dbContext = new SalesContext();
        }

        // GET: Sales
        public ActionResult Index()
        {
            List<Product> productList = dbContext.Products.ToList();
            ViewBag.Products = new SelectList(productList, "ProductId", "Name");

            List<Customer> customerList = dbContext.Customers.ToList();
            ViewBag.Customers = new SelectList(customerList, "CustomerId", "Name");

            List<Store> storeList = dbContext.Stores.ToList();
            ViewBag.Stores = new SelectList(storeList, "StoreId", "Name");

            return View();
        }

        public JsonResult GetSalesList()
        {
            List<ViewModel.SalesViewModel> list = dbContext.ProdcutSales.Select(x => new ViewModel.SalesViewModel
            {
                Id = x.Id,
                ProductId = x.Product.ProductId,
                CustomerId = x.CustomerId,
                StoreId = x.StoreId,
                DateSold = x.DateSold,
                ProductName = x.Product.Name,
                CustomerName = x.Customer.Name,
                StoreName = x.Store.Name
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSalesById(int salesId)
        {
            var sales = dbContext.ProdcutSales.FirstOrDefault(x => x.Id == salesId);

            string value = string.Empty;
            value = JsonConvert.SerializeObject(sales, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveData(ViewModel.SalesViewModel model)
        {
            var result = false;
            try
            {
                if (model.Id > 0)
                {
                    var sales = dbContext.ProdcutSales.FirstOrDefault(x => x.Id == model.Id);
                    sales.ProductId = model.ProductId;
                    sales.CustomerId = model.CustomerId;
                    sales.StoreId = model.StoreId;
                    sales.DateSold = model.DateSold;
                    dbContext.SaveChanges();
                    result = true;
                }
                else
                {
                    ProductSold sales = new ProductSold();
                    sales.ProductId = model.ProductId;
                    sales.CustomerId = model.CustomerId;
                    sales.StoreId = model.StoreId;
                    sales.DateSold = model.DateSold;
                    dbContext.ProdcutSales.Add(sales);
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

        public JsonResult DeleteSales(int salesId)
        {
            bool result = false;
            var sales = dbContext.ProdcutSales.FirstOrDefault(x => x.Id == salesId);
            if (sales != null)
            {
                dbContext.ProdcutSales.Remove(sales);
                dbContext.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
