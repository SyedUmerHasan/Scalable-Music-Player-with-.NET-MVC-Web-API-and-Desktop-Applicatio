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

    public class RecentlyPlayedsController : Controller
    {
        private IPT_Course_Project_DB db = new IPT_Course_Project_DB();

        // GET: RecentlyPlayeds
        public ActionResult Index()
        {
            var recentlyPlayeds = db.RecentlyPlayeds.Include(r => r.Song).Include(r => r.User);
            return View(recentlyPlayeds.ToList());
        }

        // GET: RecentlyPlayeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecentlyPlayed recentlyPlayed = db.RecentlyPlayeds.Find(id);
            if (recentlyPlayed == null)
            {
                return HttpNotFound();
            }
            return View(recentlyPlayed);
        }

        // GET: RecentlyPlayeds/Create
        public ActionResult Create()
        {
            ViewBag.Song_Id = new SelectList(db.Songs, "Song_Id", "Song_Name");
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "User_Name");
            return View();
        }

        // POST: RecentlyPlayeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecentlyPlayed_Id,Song_Id,User_Id")] RecentlyPlayed recentlyPlayed)
        {
            if (ModelState.IsValid)
            {
                db.RecentlyPlayeds.Add(recentlyPlayed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Song_Id = new SelectList(db.Songs, "Song_Id", "Song_Name", recentlyPlayed.Song_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "User_Name", recentlyPlayed.User_Id);
            return View(recentlyPlayed);
        }

        // GET: RecentlyPlayeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecentlyPlayed recentlyPlayed = db.RecentlyPlayeds.Find(id);
            if (recentlyPlayed == null)
            {
                return HttpNotFound();
            }
            ViewBag.Song_Id = new SelectList(db.Songs, "Song_Id", "Song_Name", recentlyPlayed.Song_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "User_Name", recentlyPlayed.User_Id);
            return View(recentlyPlayed);
        }

        // POST: RecentlyPlayeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecentlyPlayed_Id,Song_Id,User_Id")] RecentlyPlayed recentlyPlayed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recentlyPlayed).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Song_Id = new SelectList(db.Songs, "Song_Id", "Song_Name", recentlyPlayed.Song_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "User_Name", recentlyPlayed.User_Id);
            return View(recentlyPlayed);
        }

        // GET: RecentlyPlayeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecentlyPlayed recentlyPlayed = db.RecentlyPlayeds.Find(id);
            if (recentlyPlayed == null)
            {
                return HttpNotFound();
            }
            return View(recentlyPlayed);
        }

        // POST: RecentlyPlayeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecentlyPlayed recentlyPlayed = db.RecentlyPlayeds.Find(id);
            db.RecentlyPlayeds.Remove(recentlyPlayed);
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
