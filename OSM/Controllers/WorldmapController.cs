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

        // GET: api/Worldmap?managerID=5
        public IQueryable<LandJson> GetLandHistories(int managerID)
        {
            // TODO: haal alleen de 'speciale' landen op, by default niks.
            var alleLanden = db.Landen.ToList().Select(land => new LandHistorie { Land = land });
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

        // GET: api/Worldmap?managerId=5&landId=3
        /// <summary>
        /// Haal de data over de manager in dit land op.
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="landId"></param>
        /// <returns>Landnaam, aantal keer competitie/beker/doelstelling bereikt</returns>
        [ResponseType(typeof(LandHistorieJson))]
        public IHttpActionResult GetLandHistorie(int managerId, int landId)
        {
            LandHistorie landHistorie =
                db.LandHistories.Include(l => l.Land).SingleOrDefault(land => land.Land.ID == landId && land.Manager.ID == managerId);
            if (landHistorie == null)
            {
                Land land = db.Landen.Find(landId);
                if (land != null)
                {
                    return Ok(new LandHistorieJson
                    {
                        LandNaam = land.Naam,
                        CompetitieGewonnen = 0,
                        BekerGewonnen = 0,
                        DoelstellingBehaald = 0
                    });
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return Ok(new LandHistorieJson
                {
                    LandNaam = landHistorie.Land.Naam,
                    CompetitieGewonnen = landHistorie.CompetitieGewonnen,
                    BekerGewonnen = landHistorie.BekerGewonnen,
                    DoelstellingBehaald = landHistorie.DoelstellingBehaald
                });
            }

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

    public class LandHistorieJson
    {
        public string LandNaam { get; set; }
        public int CompetitieGewonnen { get; set; }
        public int BekerGewonnen { get; set; }
        public int DoelstellingBehaald { get; set; }
    }
}