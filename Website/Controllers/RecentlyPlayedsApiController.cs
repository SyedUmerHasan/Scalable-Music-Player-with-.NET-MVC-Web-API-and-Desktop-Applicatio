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
using Website.Models;

namespace Website.Controllers
{
    public class RecentlyPlayedsApiController : ApiController
    {
        private IPT_Course_Project_DB db = new IPT_Course_Project_DB();

        // GET: api/RecentlyPlayedsApi
        public IQueryable<RecentlyPlayed> GetRecentlyPlayeds()
        {
            return db.RecentlyPlayeds;
        }

        // GET: api/RecentlyPlayedsApi/5
        [ResponseType(typeof(RecentlyPlayed))]
        public IHttpActionResult GetRecentlyPlayed(int id)
        {
            RecentlyPlayed recentlyPlayed = db.RecentlyPlayeds.Find(id);
            if (recentlyPlayed == null)
            {
                return NotFound();
            }

            return Ok(recentlyPlayed);
        }

        // PUT: api/RecentlyPlayedsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecentlyPlayed(int id, RecentlyPlayed recentlyPlayed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recentlyPlayed.RecentlyPlayed_Id)
            {
                return BadRequest();
            }

            db.Entry(recentlyPlayed).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecentlyPlayedExists(id))
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

        // POST: api/RecentlyPlayedsApi
        [ResponseType(typeof(RecentlyPlayed))]
        public IHttpActionResult PostRecentlyPlayed(RecentlyPlayed recentlyPlayed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RecentlyPlayeds.Add(recentlyPlayed);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = recentlyPlayed.RecentlyPlayed_Id }, recentlyPlayed);
        }

        // DELETE: api/RecentlyPlayedsApi/5
        [ResponseType(typeof(RecentlyPlayed))]
        public IHttpActionResult DeleteRecentlyPlayed(int id)
        {
            RecentlyPlayed recentlyPlayed = db.RecentlyPlayeds.Find(id);
            if (recentlyPlayed == null)
            {
                return NotFound();
            }

            db.RecentlyPlayeds.Remove(recentlyPlayed);
            db.SaveChanges();

            return Ok(recentlyPlayed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecentlyPlayedExists(int id)
        {
            return db.RecentlyPlayeds.Count(e => e.RecentlyPlayed_Id == id) > 0;
        }
    }
}