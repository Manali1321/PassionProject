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
using PassionProject.Models.ViewModels;

namespace PassionProject.Controllers
{
    public class StoreController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static StoreController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44376/api/storedata/");
        }
        // GET: Store/List
        public ActionResult List()
        {
            //objective: communication with store data api to retrieve a list of store
            //curl https://localhost:44376/api/storedata/liststores
            string url = "liststores";
            HttpResponseMessage response=client.GetAsync(url).Result;
            IEnumerable<StoreDto> stores = response.Content.ReadAsAsync<IEnumerable<StoreDto>>().Result;
            return View(stores);
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            //objective: communication with store data api to retrieve a list of store
            //curl https://localhost:44376/api/storedata/findstore/{id}
            string url = "findstore/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            StoreDto selectedstore = response.Content.ReadAsAsync<StoreDto>().Result;
            return View(selectedstore);
        }
        public ActionResult Error()
        {

            return View();
        }
        // GET: Store/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Store/Create
        [HttpPost]
        public ActionResult Create(Store store)
        {
            Debug.WriteLine(store.Name);
            //objective: add a new store into our system using the API
            //curl -H "Content-Type:application/json" -d @store.json https://localhost:44376/api/storedata/addstore
            string url = "addstore";


            string jsonpayload = jss.Serialize(store);
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

        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            //the existing order information
            string url = "findstore/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            StoreDto SelectedStore = response.Content.ReadAsAsync<StoreDto>().Result;
            return View(SelectedStore);
        }

        // POST: Store/Update/5
        [HttpPost]
        public ActionResult Update(int id, Store store)
        {
            string url = "UpdateStore/" + id;
            string jsonpayload = jss.Serialize(store);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Store/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findstore/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            StoreDto selectedStore = response.Content.ReadAsAsync<StoreDto>().Result;
            return View(selectedStore);
        }

        // POST: Store/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "DeleteStore/" + id;
            HttpContent content = new StringContent("");
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
    }
}
