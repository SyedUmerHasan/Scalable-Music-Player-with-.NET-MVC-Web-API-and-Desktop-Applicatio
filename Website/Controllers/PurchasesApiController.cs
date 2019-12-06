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
    public class PurchasesApiController : ApiController
    {
        private IPT_Course_Project_DB db = new IPT_Course_Project_DB();

        // GET: api/PurchasesApi
        public IQueryable<Purchase> GetPurchases()
        {
            return db.Purchases;
        }

        // GET: api/PurchasesApi/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult GetPurchase(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        // PUT: api/PurchasesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase(int id, Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.Purchase_Id)
            {
                return BadRequest();
            }

            db.Entry(purchase).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/PurchasesApi
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult PostPurchase(Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Purchases.Add(purchase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase.Purchase_Id }, purchase);
        }

        // DELETE: api/PurchasesApi/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult DeletePurchase(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return NotFound();
            }

            db.Purchases.Remove(purchase);
            db.SaveChanges();

            return Ok(purchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseExists(int id)
        {
            return db.Purchases.Count(e => e.Purchase_Id == id) > 0;
        }
    }
}