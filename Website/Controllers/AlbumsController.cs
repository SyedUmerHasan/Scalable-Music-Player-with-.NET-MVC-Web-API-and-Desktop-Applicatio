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
    public class AlbumsController : Controller
    {
        private IPT_Course_Project_DB db = new IPT_Course_Project_DB();

        // GET: Albums
        public ActionResult Index()
        {
            return View(db.Albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Album_Id,Album_Name,ImageFile")] Album album)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(album.ImageFile.FileName);
                string extension = Path.GetExtension(album.ImageFile.FileName);

                // Removes Special chaeracters and encode in URL
                fileName = Regex.Replace(fileName, "[^a-zA-Z0-9_]+", "");

                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                album.Album_Image_Url = "/Images/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                album.ImageFile.SaveAs(fileName);

                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Album_Id,Album_Name,ImageFile")] Album album)
        {
            if (ModelState.IsValid)
            {
                
                    string fileName = Path.GetFileNameWithoutExtension(album.ImageFile.FileName);
                    string extension = Path.GetExtension(album.ImageFile.FileName);
                    // Removes Special chaeracters and encode in URL
                    fileName = Regex.Replace(fileName, "[^a-zA-Z0-9_]+", "");

                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    album.Album_Image_Url = "/Images/" + fileName;

                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    album.ImageFile.SaveAs(fileName);

                    db.Entry(album).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
               
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
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
