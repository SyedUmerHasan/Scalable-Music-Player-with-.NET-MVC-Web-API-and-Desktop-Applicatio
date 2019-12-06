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
    public class DownloadsApiController : ApiController
    {
        private IPT_Course_Project_DB db = new IPT_Course_Project_DB();

        // GET: api/DownloadsApi
        public IQueryable<Download> GetDownloads()
        {
            return db.Downloads;
        }

        // GET: api/DownloadsApi/5
        [ResponseType(typeof(Download))]
        public IHttpActionResult GetDownload(int id)
        {
            Download download = db.Downloads.Find(id);
            if (download == null)
            {
                return NotFound();
            }

            return Ok(download);
        }

        // PUT: api/DownloadsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDownload(int id, Download download)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != download.Download_Id)
            {
                return BadRequest();
            }

            db.Entry(download).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DownloadExists(id))
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

        // POST: api/DownloadsApi
        [ResponseType(typeof(Download))]
        public IHttpActionResult PostDownload(Download download)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Downloads.Add(download);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = download.Download_Id }, download);
        }

        // DELETE: api/DownloadsApi/5
        [ResponseType(typeof(Download))]
        public IHttpActionResult DeleteDownload(int id)
        {
            Download download = db.Downloads.Find(id);
            if (download == null)
            {
                return NotFound();
            }

            db.Downloads.Remove(download);
            db.SaveChanges();

            return Ok(download);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DownloadExists(int id)
        {
            return db.Downloads.Count(e => e.Download_Id == id) > 0;
        }
    }
}