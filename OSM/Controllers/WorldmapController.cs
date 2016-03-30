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
            var achievementLanden = db.LandHistories.Include(l => l.Land).Where(land => land.Manager.ID == managerID);
            var result = new List<LandJson>();

            foreach (var land in achievementLanden)
            {
                int status = 1;
                if (land.CompetitieGewonnen > 0)
                {
                    status = 4;
                }
                else if (land.BekerGewonnen > 0)
                {
                    status = 3;
                }
                else if (land.DoelstellingBehaald > 0)
                {
                    status = 2;
                }
                result.Add(new LandJson { LandIso = land.Land.IsoCode, Naam = land.Land.Naam, Status = status });
            }
            
            return result.AsQueryable();
        }

        // GET: api/Worldmap?managerId=5&landIso=NL
        /// <summary>
        /// Haal de data over de manager in dit land op.
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="landIso"></param>
        /// <returns>Landnaam, aantal keer competitie/beker/doelstelling bereikt</returns>
        [ResponseType(typeof(LandHistorieJson))]
        public IHttpActionResult GetLandHistorie(int managerId, string landIso)
        {
            // FirstOrDefault zou SingleOrDefault mogen zijn, maar graag pas na toevoegen unique index op Land en Manager in LandHistorie
            LandHistorie landHistorie =
                db.LandHistories.Include(l => l.Land)
                .FirstOrDefault(land => land.Land.IsoCode == landIso && land.Manager.ID == managerId);
            if (landHistorie == null)
            {
                Land land = db.Landen.SingleOrDefault(l => l.IsoCode == landIso);
                if (land != null)
                {
                    return Ok(new LandHistorieJson
                    {
                        LandNaam = land.Naam,
                        LandIso = land.IsoCode,
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
                    LandIso = landHistorie.Land.IsoCode,
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
    }

    public class LandJson
    {
        public string Naam { get; set; }
        public string LandIso { get; set; }
        public int Status { get; set; }
    }

    public class LandHistorieJson
    {
        public string LandNaam { get; set; }
        public string LandIso { get; set; }
        public int CompetitieGewonnen { get; set; }
        public int BekerGewonnen { get; set; }
        public int DoelstellingBehaald { get; set; }
    }
}