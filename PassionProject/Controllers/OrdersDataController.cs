using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject.Migrations;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class OrdersDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/OrdersData/ListOrders
        [HttpGet]
        public IEnumerable<OrderDto> ListOrders()
        {
            List<Order> Orders = db.Orders.ToList();
            List<OrderDto> OrdersDtos = new List<OrderDto>();

            Orders.ForEach(o => OrdersDtos.Add(new OrderDto(){
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                Quantity = o.Quantity,
                StoreId = o.Store.StoreID,
                ProductId = o.Grocery.ProductId
            }));

            return OrdersDtos;
        }
        /// <summary>
        /// Gathers information about all orders related to a particular grocery id
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all orders in the database, including their associated grocery matched with a particular grocery id
        /// </returns>
        /// <param name="id">grocery id</param>
        /// <example>
        /// GET: api/ordersdata/ListOrdersForGrocery/12
        /// </example>
        [HttpGet]
        [ResponseType(typeof(OrderDto))]
        public IHttpActionResult ListOrdersForGrocery(int id)
        {
            List<Order> Orders = db.Orders.Where(a => a.ProductId == id).ToList();
            List<OrderDto> orderDtos = new List<OrderDto>();

            Orders.ForEach(a => orderDtos.Add(new OrderDto()
            {
                Id = a.Id,
                Quantity = a.Quantity,
                StoreId = a.Store.StoreID,
                OrderNumber = a.OrderNumber,
                ProductId = a.Grocery.ProductId
            }));

            return Ok(orderDtos);
        }
        /// <summary>
        /// Gathers information about all orders related to a particular store id
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all orders in the database, including their associated store matched with a store id
        /// </returns>
        /// <param name="id">store id</param>
        /// <example>
        /// GET: api/ordersdata/ListOrdersForStore/6
        /// </example>
        [HttpGet]
        [ResponseType(typeof(OrderDto))]
        public IHttpActionResult ListOrdersForStore(int id)
        {
            List<Order> Orders = db.Orders.Where(a => a.StoreID == id).ToList();
            List<OrderDto> orderDtos = new List<OrderDto>();

            Orders.ForEach(a => orderDtos.Add(new OrderDto()
            {
                Id = a.Id,
                Quantity = a.Quantity,
                StoreId = a.Store.StoreID,
                OrderNumber = a.OrderNumber,
                ProductId = a.Grocery.ProductId
            }));

            return Ok(orderDtos);
        }

        // GET: api/OrdersData/FindOrder/5
        [ResponseType(typeof(Order))]
        [HttpGet]
        public IHttpActionResult FindOrder(int id)
        {
            Order order = db.Orders.Find(id);
            OrderDto OrderDto=new OrderDto()
            {
                Id = order.Id,
                OrderNumber= order.OrderNumber,
                Quantity= order.Quantity,
                StoreId= order.Store.StoreID,
                ProductId= order.Grocery.ProductId
            };
            return Ok(OrderDto);
        }
        
        // POST: api/OrderData/UpdateOrder/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OrdersData/AddOrder
        [ResponseType(typeof(Order))]
        [HttpPost]
        public IHttpActionResult AddOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // POST: api/OrdersData/DeleteOrder/5
        [ResponseType(typeof(Order))]
        [HttpPost]

        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}