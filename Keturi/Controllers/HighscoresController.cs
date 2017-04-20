using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Keturi.Models;

namespace Keturi.Controllers
{
    public class HighscoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Highscores
        public ActionResult Index()
        {
            return View(db.Highscores.ToList());
        }

        // GET: Highscores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Highscore highscore = db.Highscores.Find(id);
            if (highscore == null)
            {
                return HttpNotFound();
            }
            return View(highscore);
        }

        // GET: Highscores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Highscores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nickname,Score,ApplicationUserId")] Highscore highscore)
        {
            if (ModelState.IsValid)
            {
                db.Highscores.Add(highscore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(highscore);
        }

        // GET: Highscores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Highscore highscore = db.Highscores.Find(id);
            if (highscore == null)
            {
                return HttpNotFound();
            }
            return View(highscore);
        }

        // POST: Highscores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nickname,Score,ApplicationUserId")] Highscore highscore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(highscore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(highscore);
        }

        // GET: Highscores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Highscore highscore = db.Highscores.Find(id);
            if (highscore == null)
            {
                return HttpNotFound();
            }
            return View(highscore);
        }

        // POST: Highscores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Highscore highscore = db.Highscores.Find(id);
            db.Highscores.Remove(highscore);
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
