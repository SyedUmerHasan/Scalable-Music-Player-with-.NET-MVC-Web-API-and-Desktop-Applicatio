using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Website.Models;

namespace Website.Controllers
{
    public class SongsApiController : ApiController
    {
        private IPT_Course_Project_DB db = new IPT_Course_Project_DB();

        // GET: api/SongsApi
        public IQueryable<Song> GetSongs()
        {
            return db.Songs;
        }

        // GET: api/SongsApi/5
        [ResponseType(typeof(Song))]
        public IHttpActionResult GetSong(int id)
        {
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        // PUT: api/SongsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSong(int id, Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != song.Song_Id)
            {
                return BadRequest();
            }

            db.Entry(song).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // POST: api/SongsApi
        [ResponseType(typeof(Song))]
        public IHttpActionResult PostSong(Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            string fileName = Path.GetFileNameWithoutExtension(song.ImageFile.FileName);
            string extension = Path.GetExtension(song.ImageFile.FileName);

            // Removes Special characters and encode in URL
            fileName = Regex.Replace(fileName, "[^a-zA-Z0-9_]+", "");

            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            song.Song_Image_Url = "/Images/" + fileName;

            fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), fileName);
            song.ImageFile.SaveAs(fileName);


            string fileName_Song_Url = Path.GetFileNameWithoutExtension(song.Song_Url_Link.FileName);
            string extension_Song_Url = Path.GetExtension(song.Song_Url_Link.FileName);

            // Removes Special characters and encode in URL
            fileName_Song_Url = Regex.Replace(fileName_Song_Url, "[^a-zA-Z0-9_]+", "");

            fileName_Song_Url = fileName_Song_Url + DateTime.Now.ToString("yymmssfff") + extension_Song_Url;

            song.Song_Url = "/Songs/" + fileName_Song_Url;

            fileName_Song_Url = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/Songs/"), fileName_Song_Url);
            song.Song_Url_Link.SaveAs(fileName_Song_Url);


            db.Songs.Add(song);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = song.Song_Id }, song);
        }

        // DELETE: api/SongsApi/5
        [ResponseType(typeof(Song))]
        public IHttpActionResult DeleteSong(int id)
        {
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return NotFound();
            }

            db.Songs.Remove(song);
            db.SaveChanges();

            return Ok(song);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SongExists(int id)
        {
            return db.Songs.Count(e => e.Song_Id == id) > 0;
        }
    }
}