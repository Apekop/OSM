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
using OSM;
using OSM.Models;

namespace OSM.Controllers
{
    public class LandController : ApiController
    {
        private OSMContext db = new OSMContext();

        // GET: api/Land
        public IQueryable<Land> GetLanden()
        {
            return db.Landen;
        }

        // GET: api/Land/5
        [ResponseType(typeof(Land))]
        public IHttpActionResult GetLand(int id)
        {
            Land land = db.Landen.Find(id);
            if (land == null)
            {
                return NotFound();
            }

            return Ok(land);
        }

        // PUT: api/Land/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLand(int id, Land land)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != land.ID)
            {
                return BadRequest();
            }

            db.Entry(land).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LandExists(id))
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

        // POST: api/Land
        [ResponseType(typeof(Land))]
        public IHttpActionResult PostLand(Land land)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Landen.Add(land);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = land.ID }, land);
        }

        // DELETE: api/Land/5
        [ResponseType(typeof(Land))]
        public IHttpActionResult DeleteLand(int id)
        {
            Land land = db.Landen.Find(id);
            if (land == null)
            {
                return NotFound();
            }

            db.Landen.Remove(land);
            db.SaveChanges();

            return Ok(land);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LandExists(int id)
        {
            return db.Landen.Count(e => e.ID == id) > 0;
        }
    }
}