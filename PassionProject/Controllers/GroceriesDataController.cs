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

namespace PassionProject.Controllers
{
    public class GroceriesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Groceries/ListGroceries
        [HttpGet]
        public IEnumerable<GroceryDto> ListGroceries()
        {
            List<Grocery> Groceries =db.Groceries.ToList();
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

        /*// GET: api/Groceries/5
        [ResponseType(typeof(Grocery))]
        public IHttpActionResult GetGrocery(int id)
        {
            Grocery grocery = db.Groceries.Find(id);
            if (grocery == null)
            {
                return NotFound();
            }

            return Ok(grocery);
        }

        // PUT: api/Groceries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrocery(int id, Grocery grocery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grocery.ProductId)
            {
                return BadRequest();
            }

            db.Entry(grocery).State = EntityState.Modified;

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
        public IHttpActionResult PostGrocery(Grocery grocery)
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

            return Ok(grocery);
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
        }*/
    }
}