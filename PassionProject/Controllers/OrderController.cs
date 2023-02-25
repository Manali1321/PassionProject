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
    public class OrderController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static OrderController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44376/api/");
        }
        //GET: Order/List
        public ActionResult List()
        {
            //objective: communication with store data api to retrieve a list of store
            //curl https://localhost:44376/api/orderdata/listorders

            string url = "OrdersData/ListOrders";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("This response code is");
            //Debug.WriteLine(response);

            IEnumerable<OrderDto> orders = response.Content.ReadAsAsync<IEnumerable<OrderDto>>().Result;
            //Debug.WriteLine("number of store");
            //Debug.WriteLine(orders.Count());
            return View(orders);
        }

        // GET: order/Details/5
        public ActionResult Details(int id)
        {
            //objective: communication with store data api to retrieve a list of store
            //curl https://localhost:44376/api/ordersdata/findorder/{id}

            string url = "OrdersData/findorder/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            OrderDto selectedorder = response.Content.ReadAsAsync<OrderDto>().Result;
            
            return View(selectedorder);
        }
        //Error
        public ActionResult Error()
        {

            return View();
        }
        // GET: order/New
        public ActionResult New()
        {
            string url = "storedata/liststores";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<StoreDto> StoreOptions = response.Content.ReadAsAsync<IEnumerable<StoreDto>>().Result;


            string url1 = "Groceriesdata/Listgroceries";
            HttpResponseMessage response1 = client.GetAsync(url1).Result;
            IEnumerable<GroceryDto> GroceryOptions = response1.Content.ReadAsAsync<IEnumerable<GroceryDto>>().Result;


            var viewModel = new MyViewModel
            {
                StoreOptions = StoreOptions,
                GroceryOptions = GroceryOptions
            };
            return View(viewModel);
        }
        
        // POST: order/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
            //Debug.WriteLine("the json payload is :");
            Debug.WriteLine(order);
            //objective: add a new animal into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/OrdersData/AddOrder 
            string url = "ordersdata/addorder";


            string jsonpayload = jss.Serialize(order);
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
                return RedirectToAction("List");
            }
        }

        // GET: order/Edit/5
        public ActionResult Edit(int id)
        {

            UpdateOrder ViewModel = new UpdateOrder();

            //the existing order information
            string url = "ordersdata/findorder/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            OrderDto SelectedOrder = response.Content.ReadAsAsync<OrderDto>().Result;
            ViewModel.SelectedOrder = SelectedOrder;

            // all grocery to choose from when updating this grocery
            //the existing grocery information
            url = "Groceriesdata/Listgroceries";
            response = client.GetAsync(url).Result;
            IEnumerable<GroceryDto> GroceryOptions = response.Content.ReadAsAsync<IEnumerable<GroceryDto>>().Result;

            // all store to choose from when updating this store
            //the existing store information
            url = "storedata/liststores";
            response = client.GetAsync(url).Result;
            IEnumerable<StoreDto> StoreOptions = response.Content.ReadAsAsync<IEnumerable<StoreDto>>().Result;
            ViewModel.StoreOptions = StoreOptions;


            ViewModel.SelectedOrder = SelectedOrder;
            ViewModel.StoreOptions = StoreOptions;
            ViewModel.GroceryOptions = GroceryOptions;

            return View(ViewModel);

        }

        // POST: order/Update/5
        [HttpPost]  
        public ActionResult Update(int id, Order order)
        {
            Debug.WriteLine(id);
            string url = "ordersdata/updateorder/" + id;
            string jsonpayload = jss.Serialize(order);
            Debug.WriteLine(jsonpayload);
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

        // GET: order/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "OrdersData/findorder/" + id; ;
            HttpResponseMessage response = client.GetAsync(url).Result;
            OrderDto selectedorder = response.Content.ReadAsAsync<OrderDto>().Result;
            return View(selectedorder);
        }

        // POST: order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "OrdersData/deleteorder/" + id;
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
