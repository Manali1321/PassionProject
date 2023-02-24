using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using PassionProject.Migrations;
using PassionProject.Models;
using System.Web.Script.Serialization;

namespace PassionProject.Controllers
{
    public class groceryController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static groceryController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44376/api/groceriesdata/");
        }
        // GET: grocery/List
        public ActionResult List()
        {
            //objective: communication with store data api to retrieve a list of store
            //curl https://localhost:44376/api/groceriesdata/listgroceries

            string url = "listgroceries";
            HttpResponseMessage response = client.GetAsync(url).Result;


            IEnumerable<GroceryDto> grocery = response.Content.ReadAsAsync<IEnumerable<GroceryDto>>().Result;
            Debug.WriteLine("number of store");
            Debug.WriteLine(grocery.Count());
            return View(grocery);
        }

        // GET: grocery/Details/12
        public ActionResult Details(int id)
        {
            //objective: communication with store data api to retrieve a list of store
            //curl https://localhost:44376/api/groceriesdata/findgrocery/{id}

            string url = "findgrocery/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            GroceryDto selectedgrocery = response.Content.ReadAsAsync<GroceryDto>().Result;
   
            return View(selectedgrocery);
        }
        public ActionResult Error()
        {

            return View();
        }

        // GET: grocery/New
        public ActionResult New()
        {
            return View();
        }

        // POST: grocery/Create
        [HttpPost]
        public ActionResult Create(Grocery grocery)
        {
            Debug.WriteLine(grocery.Upc);
            //objective: add a new store into our system using the API
            //curl -H "Content-Type:application/json" -d @grocery.json https://localhost:44376/api/groceriesdata/addGrocery
            string url = "addGrocery";


            string jsonpayload = jss.Serialize(grocery);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: grocery/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: grocery/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: grocery/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: grocery/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
