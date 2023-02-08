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
    public class StoreDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/StoreData/ListStores
        [HttpGet]
        public IEnumerable<StoreDto> ListStores()
        {
            List<Store> stores = db.Stores.ToList();
            List<StoreDto> StoreDtos = new List<StoreDto>();

            stores.ForEach(s => StoreDtos.Add(new StoreDto()
            {
                StoreID = s.StoreID,
                Name = s.Name

            }));

            return StoreDtos;
        }

        // GET: api/StoreData//FindStore/5
        [ResponseType(typeof(Store))]
        [HttpGet]
        public IHttpActionResult FindStore(int id)
        {
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // POST: api/StoreData/UpdateStore/5
       /* [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult PutStore(int id, Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != store.StoreID)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/StoreData
        [ResponseType(typeof(Store))]
        public IHttpActionResult PostStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stores.Add(store);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = store.StoreID }, store);
        }

        // DELETE: api/StoreData/5
        [ResponseType(typeof(Store))]
        public IHttpActionResult DeleteStore(int id)
        {
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            db.Stores.Remove(store);
            db.SaveChanges();

            return Ok(store);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoreExists(int id)
        {
            return db.Stores.Count(e => e.StoreID == id) > 0;
        }*/
    }
}