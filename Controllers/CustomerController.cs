﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD.Models;
using Newtonsoft.Json;

namespace CRUD.Controllers
{
    public class CustomerController : Controller
    {
        SalesContext dbContext;

        public CustomerController()
        {
            dbContext = new SalesContext();
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCustomerList()
        {
            List<ViewModel.CustomerViewModel> customerList = dbContext.Customers.Select(x => new ViewModel.CustomerViewModel
            {
                Id = x.CustomerId,
                Name = x.Name,
                Age = x.Age,
                Address = x.Address
            }).ToList();

            return Json(customerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerById(int customerId)
        {
            var customer = dbContext.Customers.FirstOrDefault(x => x.CustomerId == customerId);

            string value = string.Empty;
            value = JsonConvert.SerializeObject(customer, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveData(ViewModel.CustomerViewModel model)
        {
            var result = false;
            try
            {
                if (model.Id > 0)
                {
                    var customer = dbContext.Customers.FirstOrDefault(x => x.CustomerId == model.Id);
                    customer.Name = model.Name;
                    customer.Age = model.Age;
                    customer.Address = model.Address;
                    dbContext.SaveChanges();
                    result = true;
                }
                else
                {
                    Customer customer = new Customer();
                    customer.Name = model.Name;
                    customer.Age = model.Age;
                    customer.Address = model.Address;
                    dbContext.Customers.Add(customer);
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

        public JsonResult DeleteCustomer(int customerId)
        {
            bool result = false;
            var customer = dbContext.Customers.FirstOrDefault(x => x.CustomerId == customerId);
            if (customer != null)
            {
                dbContext.Customers.Remove(customer);
                dbContext.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
