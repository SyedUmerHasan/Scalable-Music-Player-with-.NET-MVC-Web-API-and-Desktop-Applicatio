using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    [RoutePrefix("dashboard")]

    public class SongsController : Controller
    {
        private IPT_Course_Project_DB db = new IPT_Course_Project_DB();

        // GET: Songs
        public ActionResult Index()
        {
            var songs = db.Songs.Include(s => s.Album).Include(s => s.Artist).Include(s => s.Genre);
            return View(songs.ToList());
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            ViewBag.Album_Id = new SelectList(db.Albums, "Album_Id", "Album_Name");
            ViewBag.Artist_Id = new SelectList(db.Artists, "Artist_Id", "Artist_Name");
            ViewBag.Genre_Id = new SelectList(db.Genres, "Genre_Id", "Genre_Name");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Song_Id,Artist_Id,Album_Id,Genre_Id,Song_Name,ImageFile,Song_Playtime,Song_Price,Song_Url_Link")] Song song)
        {
            if (ModelState.IsValid)
            {

                string fileName = Path.GetFileNameWithoutExtension(song.ImageFile.FileName);
                string extension = Path.GetExtension(song.ImageFile.FileName);

                // Removes Special characters and encode in URL
                fileName = Regex.Replace(fileName, "[^a-zA-Z0-9_]+", "");

                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                song.Song_Image_Url = "/Images/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                song.ImageFile.SaveAs(fileName);


                string fileName_Song_Url = Path.GetFileNameWithoutExtension(song.Song_Url_Link.FileName);
                string extension_Song_Url = Path.GetExtension(song.Song_Url_Link.FileName);

                // Removes Special characters and encode in URL
                fileName_Song_Url = Regex.Replace(fileName_Song_Url, "[^a-zA-Z0-9_]+", "");

                fileName_Song_Url = fileName_Song_Url + DateTime.Now.ToString("yymmssfff") + extension_Song_Url;

                song.Song_Url = "/Songs/" + fileName_Song_Url;

                fileName_Song_Url = Path.Combine(Server.MapPath("~/Songs/"), fileName_Song_Url);
                song.Song_Url_Link.SaveAs(fileName_Song_Url);


                db.Songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Album_Id = new SelectList(db.Albums, "Album_Id", "Album_Name", song.Album_Id);
            ViewBag.Artist_Id = new SelectList(db.Artists, "Artist_Id", "Artist_Name", song.Artist_Id);
            ViewBag.Genre_Id = new SelectList(db.Genres, "Genre_Id", "Genre_Name", song.Genre_Id);
            return View(song);
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.Album_Id = new SelectList(db.Albums, "Album_Id", "Album_Name", song.Album_Id);
            ViewBag.Artist_Id = new SelectList(db.Artists, "Artist_Id", "Artist_Name", song.Artist_Id);
            ViewBag.Genre_Id = new SelectList(db.Genres, "Genre_Id", "Genre_Name", song.Genre_Id);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Song_Id,Artist_Id,Album_Id,Genre_Id,Song_Name,ImageFile,Song_Playtime,Song_Price,Song_Url_Link")] Song song)
        {
            if (ModelState.IsValid)
            {


                string fileName = Path.GetFileNameWithoutExtension(song.ImageFile.FileName);
                string extension = Path.GetExtension(song.ImageFile.FileName);
                // Removes Special characters and encode in URL
                fileName = Regex.Replace(fileName, "[^a-zA-Z0-9_]+", "");

                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                song.Song_Image_Url = "/Images/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                song.ImageFile.SaveAs(fileName);




                string fileName_Song_Url = Path.GetFileNameWithoutExtension(song.Song_Url_Link.FileName);
                string extension_Song_Url = Path.GetExtension(song.Song_Url_Link.FileName);

                // Removes Special characters and encode in URL
                fileName_Song_Url = Regex.Replace(fileName_Song_Url, "[^a-zA-Z0-9_]+", "");

                fileName_Song_Url = fileName_Song_Url + DateTime.Now.ToString("yymmssfff") + extension_Song_Url;

                song.Song_Url = "/Songs/" + fileName_Song_Url;

                fileName_Song_Url = Path.Combine(Server.MapPath("~/Songs/"), fileName_Song_Url);
                song.Song_Url_Link.SaveAs(fileName_Song_Url);


                db.Entry(song).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Album_Id = new SelectList(db.Albums, "Album_Id", "Album_Name", song.Album_Id);
            ViewBag.Artist_Id = new SelectList(db.Artists, "Artist_Id", "Artist_Name", song.Artist_Id);
            ViewBag.Genre_Id = new SelectList(db.Genres, "Genre_Id", "Genre_Name", song.Genre_Id);
            return View(song);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
