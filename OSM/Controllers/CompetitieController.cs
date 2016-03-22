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
    public class CompetitieController : ApiController
    {
        private OSMContext db = new OSMContext();

        // GET: api/Competitie
        public IQueryable<Competitie> GetCompetities()
        {
            return db.Competities;
        }

        // GET: api/Competitie/5
        [ResponseType(typeof(Competitie))]
        public IHttpActionResult GetCompetitie(int id)
        {
            Competitie competitie = db.Competities.Find(id);
            if (competitie == null)
            {
                return NotFound();
            }

            return Ok(competitie);
        }

        // PUT: api/Competitie/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompetitie(int id, Competitie competitie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != competitie.ID)
            {
                return BadRequest();
            }

            db.Entry(competitie).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitieExists(id))
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

        // POST: api/Competitie
        [ResponseType(typeof(Competitie))]
        public IHttpActionResult PostCompetitie(Competitie competitie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Competities.Add(competitie);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = competitie.ID }, competitie);
        }

        // DELETE: api/Competitie/5
        [ResponseType(typeof(Competitie))]
        public IHttpActionResult DeleteCompetitie(int id)
        {
            Competitie competitie = db.Competities.Find(id);
            if (competitie == null)
            {
                return NotFound();
            }

            db.Competities.Remove(competitie);
            db.SaveChanges();

            return Ok(competitie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompetitieExists(int id)
        {
            return db.Competities.Count(e => e.ID == id) > 0;
        }
    }
}