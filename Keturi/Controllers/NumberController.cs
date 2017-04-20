using Keturi.Models;
using Keturi.Extensions;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keturi.Controllers
{
    public class NumberController : Controller
    {

        // sugeneruojamas random skaicius ir sukuriamas sarasas, kuriame talpinami
        // neteisingi atsakymai

        public ActionResult Index()
        {
            Number n = new Number();
            n.Generate();
            n.createNotes(new List<string>());
            string answer = n.Answer;
            Session["answer"] = answer;
            Session["n"] = n;
            return View();
        }

        // atlikus spejima, tikrinama ar jis atitinka sugeneruota skaiciu,
        // skaiciui atitikus nukreipiama i "win" langa
        // kitu atveju patiekiama informacija apie neteisinga spejima

        [HttpPost]
        public ActionResult Index(Number model)
        {
            Number n = (Number)Session["n"];
            n.Guess = model.Guess;
            if (n.compareValues())
            {
                return RedirectToAction("Win");
            }
            else
            {
                return View(n);
            }
        }

        //Nurodoma is kiek bandymu atspeta, bei leidziama rezultata patalpinti i duombaze
        public ActionResult Win()
        {
            Number n = (Number)Session["n"];
            var db = new ApplicationDbContext();
            var highscore = new Highscore { Nickname = User.Identity.GetNickname(), Score = n.Notes.Count(), ApplicationUserId = User.Identity.GetUserId() };
            db.Highscores.Add(highscore);
            db.SaveChanges();
            return View(n);
        }
        [Authorize]
        public ActionResult Insert()
        {
            Number n = (Number)Session["n"];
            var db = new ApplicationDbContext();
            var highscore = new Highscore { Nickname = User.Identity.GetNickname(), Score = n.Notes.Count(), ApplicationUserId = User.Identity.GetUserId() };
            db.Highscores.Add(highscore);
            db.SaveChanges();
            return View();
        }
    }
}