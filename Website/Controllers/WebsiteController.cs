using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class WebsiteController : Controller
    {
        private IPT_Course_Project_DB db = new IPT_Course_Project_DB();
        // GET: Website
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Albums()
        {
            return View();
        }
        public ActionResult Artists()

        {
            return View(db.Artists.ToList());
        }
        public ActionResult Search(string id)
        {
            var data = db.Songs.Where(s => s.Song_Name.ToLower().Contains(id)).ToList();
            //  return Json(data.ToList(), JsonRequestBehavior.AllowGet);
            return View(data.ToList());
        }
        public ActionResult AddToSession(int id)
        {
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
    }
}