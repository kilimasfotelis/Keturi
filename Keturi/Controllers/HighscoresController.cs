using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Keturi.Models;
using PagedList;

namespace Keturi.Controllers
{
    public class HighscoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Patiekiami visi zaidima uzbaigusiu rezultatai (sorting, paging, filtering)
        // GET: Highscores
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ScoreSortParm = String.IsNullOrEmpty(sortOrder) ? "score_desc" : "";
            ViewBag.NicknameSortParm = sortOrder == "Nickname" ? "nickname_desc": "Nickname";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var highscores = from s in db.Highscores select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                highscores = highscores.Where(s => s.Nickname.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nickname_desc":
                    highscores = highscores.OrderByDescending(s => s.Nickname);
                    break;
                case "score_desc":
                    highscores = highscores.OrderByDescending(s => s.Score);
                    break;
                case "Nickname":
                    highscores = highscores.OrderBy(s => s.Nickname);
                    break;
                default:
                    highscores = highscores.OrderBy(s => s.Score);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(highscores.ToPagedList(pageNumber, pageSize));
        }

        // Administratorius gali keisti irasus

        // GET: Highscores/Edit/5
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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

        // Administratorius gali trinti irasus

        // GET: Highscores/Delete/5
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
