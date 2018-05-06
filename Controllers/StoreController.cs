using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD.Models;
using Newtonsoft.Json;

namespace CRUD.Controllers
{
    public class StoreController : Controller
    {
        SalesContext dbContext;

        public StoreController()
        {
            dbContext = new SalesContext();
        }

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStoreList()
        {
            List<ViewModel.StoreViewModel> list = dbContext.Stores.Select(x => new ViewModel.StoreViewModel
            {
                Id = x.StoreId,
                Name = x.Name,
                Address = x.Address
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStoreById(int storeId)
        {
            var store = dbContext.Stores.FirstOrDefault(x => x.StoreId == storeId);

            string value = string.Empty;
            value = JsonConvert.SerializeObject(store, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveData(Store model)
        {
            var result = false;
            try
            {
                if (model.StoreId > 0)
                {
                    var store = dbContext.Stores.FirstOrDefault(x => x.StoreId == model.StoreId);
                    store.Name = model.Name;
                    store.Address = model.Address;
                    dbContext.SaveChanges();
                    result = true;
                }
                else
                {
                    Store store = new Store();
                    store.Name = model.Name;
                    store.Address = model.Address;
                    dbContext.Stores.Add(store);
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

        public JsonResult DeleteStore(int storeId)
        {
            bool result = false;
            var store = dbContext.Stores.FirstOrDefault(x => x.StoreId == storeId);
            if (store != null)
            {
                dbContext.Stores.Remove(store);
                dbContext.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
