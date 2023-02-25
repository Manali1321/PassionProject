using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject.Models;
using System.Diagnostics;

namespace PassionProject.Controllers
{
    public class GroceriesDataController : ApiController
    {
        //private readonly ApplicationDbContext db;

        //public GroceriesDataController(ApplicationDbContext context)
        //{
          //  db = context;
        //}
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Groceriesdata/Listgroceries
        [HttpGet]
        public IEnumerable<GroceryDto> ListGroceries()
        {
            List<Grocery> Groceries = db.Groceries.ToList();
            List<GroceryDto> GroceriesDtos = new List<GroceryDto>();

            Groceries.ForEach(g => GroceriesDtos.Add(new GroceryDto(){
                ProductId = g.ProductId,
                Upc=g.Upc,
                Name = g.Name,
                Weight = g.Weight,
                Stock = g.Stock

            }));
            return GroceriesDtos;
        }

        // GET: api/groceriesdata/findgroceries/5
        [ResponseType(typeof(Grocery))]
        [HttpGet]
        public IHttpActionResult FindGrocery(int id)
        {
            Grocery Grocery = db.Groceries.Find(id);
            GroceryDto GroceryDto = new GroceryDto()
            {
                ProductId = Grocery.ProductId,
                Upc = Grocery.Upc,
                Name = Grocery.Name,
                Weight = Grocery.Weight,
                Stock= Grocery.Stock
            };
            if (Grocery == null)
            {
                return NotFound();
            }

            return Ok(GroceryDto);
        }

        // Post: api/GroceriesData/UpdateGrocery/5
        //curl -d @grocery.json -H "content-type:application/json" https://localhost:44376/api/groceriesdata/updategrocery/12

        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateGrocery(int id, GroceryDto groceryDto)
        {
            Debug.WriteLine("update");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != groceryDto.ProductId)
            {
                return BadRequest();
            }

            var grocery = db.Groceries.Find(id);

            if (grocery == null)
            {
                return NotFound();
            }

            grocery.Upc = groceryDto.Upc;
            grocery.Name = groceryDto.Name;
            grocery.Weight = groceryDto.Weight;
            grocery.Stock = groceryDto.Stock;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryExists(id))
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

        // POST: api/Groceries
        [ResponseType(typeof(Grocery))]
        public IHttpActionResult AddGrocery(Grocery grocery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Groceries.Add(grocery);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = grocery.ProductId }, grocery);
        }

        // DELETE: api/Groceries/5
        [ResponseType(typeof(Grocery))]
        public IHttpActionResult DeleteGrocery(int id)
        {
            Grocery grocery = db.Groceries.Find(id);
            if (grocery == null)
            {
                return NotFound();
            }

            db.Groceries.Remove(grocery);
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

        private bool GroceryExists(int id)
        {
            return db.Groceries.Count(e => e.ProductId == id) > 0;
        }
    }
}