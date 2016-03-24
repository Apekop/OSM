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
    public class WorldmapController : ApiController
    {
        private OSMContext db = new OSMContext();

        // GET: api/Worldmap
        public IQueryable<LandJson> GetLandHistories(int managerID)
        {
            var alleLanden = db.Landen.Select(land => new LandHistorie { Land = land });
            var achievementLanden = db.LandHistories.Where(land => land.Manager.ID == managerID);    // PoC voor filter van juiste manager
            var result = new List<LandJson>();

            foreach (var land in alleLanden)
            {
                int status = 1;
                if (land.Land.Beschikbaar)
                {
                    if (achievementLanden.Select(x => x.Land.ID).Contains(land.ID))
                    {
                        if (achievementLanden.First(x => x.Land.ID == land.ID).CompetitieGewonnen > 0)
                        {
                            status = 4;
                        } else if (achievementLanden.First(x => x.Land.ID == land.ID).BekerGewonnen > 0)
                        {
                            status = 3;
                        } else if (achievementLanden.First(x => x.Land.ID == land.ID).DoelstellingBehaald > 0)
                        {
                            status = 2;
                        }
                    }
                }
                result.Add(new LandJson { ID = land.ID, Naam = land.Land.Naam, Status = status });
            }
            
            return result.AsQueryable();
        }

        // GET: api/Worldmap/5
        [ResponseType(typeof(LandJson))]
        public IHttpActionResult GetLandHistorie(int id)
        {
            LandHistorie landHistorie = db.LandHistories.Find(id);
            if (landHistorie == null)
            {
                return NotFound();
            }

            int status = 1;
            if (landHistorie.CompetitieGewonnen > 0)
            {
                status = 4;
            }else if (landHistorie.BekerGewonnen > 0)
            {
                status = 3;
            } else if (landHistorie.DoelstellingBehaald > 0)
            {
                status = 2;
            }

            return Ok(new LandJson { ID = landHistorie.Land.ID, Naam = landHistorie.Land.Naam, Status = status});
        }

        // PUT: api/Worldmap/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLandHistorie(int id, LandHistorie landHistorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != landHistorie.ID)
            {
                return BadRequest();
            }

            db.Entry(landHistorie).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LandHistorieExists(id))
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

        // POST: api/Worldmap
        [ResponseType(typeof(LandHistorie))]
        public IHttpActionResult PostLandHistorie(LandHistorie landHistorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LandHistories.Add(landHistorie);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = landHistorie.ID }, landHistorie);
        }

        // DELETE: api/Worldmap/5
        [ResponseType(typeof(LandHistorie))]
        public IHttpActionResult DeleteLandHistorie(int id)
        {
            LandHistorie landHistorie = db.LandHistories.Find(id);
            if (landHistorie == null)
            {
                return NotFound();
            }

            db.LandHistories.Remove(landHistorie);
            db.SaveChanges();

            return Ok(landHistorie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LandHistorieExists(int id)
        {
            return db.LandHistories.Count(e => e.ID == id) > 0;
        }

        private object ToJsonObject(LandHistorie land)
        {
            return new {ID = land.Land.ID, Landnaam = land.Land.Naam, Status = 1};  // Status berekenen
        }
    }

    public class LandJson
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public int Status { get; set; }
    }
}