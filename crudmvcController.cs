using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using web_Api_crud.Models;

namespace web_Api_crud.Controllers
{
    public class crudmvcController : Controller
    {
        // GET: crudmvc

        HttpClient client = new HttpClient();
        public ActionResult Index()
        {

            List<Employee> emp_list = new List<Employee>();
            client.BaseAddress = new Uri("https://localhost:44396/api/Crud_API");
            var response = client.GetAsync("Crud_API");
            response.Wait();

            var test = response.Result;
            if(test.IsSuccessStatusCode)
            {
                var Display = test.Content.ReadAsAsync<List<Employee>>();
                Display.Wait();
                emp_list = Display.Result;
            }
            return View(emp_list);
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Employee emp)
        {
            client.BaseAddress = new Uri("https://localhost:44396/api/Crud_API");
            var responce = client.PostAsJsonAsync("Crud_API" ,emp);
            responce.Wait();
            var test = responce.Result;
             if(test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        public ActionResult Details(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44396/api/Crud_API");
            var response = client.GetAsync("Crud_API?id="+id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var Display = test.Content.ReadAsAsync<Employee>();
                Display.Wait();
                e = Display.Result;
            }
            return View(e);
        }
        public ActionResult Edit(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44396/api/Crud_API");
            var response = client.GetAsync("Crud_API?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var Display = test.Content.ReadAsAsync<Employee>();
                Display.Wait();
                e = Display.Result;
            }
            return View(e);
        }
        [HttpPost]
        public ActionResult Edit(Employee e) 
        {
           // Employee = e = null;
            client.BaseAddress = new Uri("https://localhost:44396/api/Crud_API");
            var responce = client.PutAsJsonAsync<Employee>("Crud_API?id",e);
            responce.Wait();
            var test = responce.Result;
            if(test.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View("Edit");
        }
        public ActionResult Delete(int id)
        {
           
            return View();
        }
        [HttpPost , ActionName("Delete")]
        public ActionResult Deleteconfirm(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44396/api/Crud_API");
            var responce = client.DeleteAsync("Crud_API/"+ id.ToString());
            responce.Wait();

            var test = responce.Result;
            if(test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Delete");
        }
    }
}