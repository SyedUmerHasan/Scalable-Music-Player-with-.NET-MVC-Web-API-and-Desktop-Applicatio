using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    [RoutePrefix("dashboard")]

    public class DownloadsController : Controller
    {
        private IPT_Course_Project_DB db = new IPT_Course_Project_DB();

        // GET: Downloads
        public ActionResult Index()
        {
            var downloads = db.Downloads.Include(d => d.Song).Include(d => d.User);
            return View(downloads.ToList());
        }

        // GET: Downloads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Download download = db.Downloads.Find(id);
            if (download == null)
            {
                return HttpNotFound();
            }
            return View(download);
        }

        // GET: Downloads/Create
        public ActionResult Create()
        {
            ViewBag.Song_Id = new SelectList(db.Songs, "Song_Id", "Song_Name");
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "User_Name");
            return View();
        }

        // POST: Downloads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Download_Id,Song_Id,User_Id")] Download download)
        {
            if (ModelState.IsValid)
            {
                db.Downloads.Add(download);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Song_Id = new SelectList(db.Songs, "Song_Id", "Song_Name", download.Song_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "User_Name", download.User_Id);
            return View(download);
        }

        // GET: Downloads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Download download = db.Downloads.Find(id);
            if (download == null)
            {
                return HttpNotFound();
            }
            ViewBag.Song_Id = new SelectList(db.Songs, "Song_Id", "Song_Name", download.Song_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "User_Name", download.User_Id);
            return View(download);
        }

        // POST: Downloads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Download_Id,Song_Id,User_Id")] Download download)
        {
            if (ModelState.IsValid)
            {
                db.Entry(download).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Song_Id = new SelectList(db.Songs, "Song_Id", "Song_Name", download.Song_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "User_Name", download.User_Id);
            return View(download);
        }

        // GET: Downloads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Download download = db.Downloads.Find(id);
            if (download == null)
            {
                return HttpNotFound();
            }
            return View(download);
        }

        // POST: Downloads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Download download = db.Downloads.Find(id);
            db.Downloads.Remove(download);
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
