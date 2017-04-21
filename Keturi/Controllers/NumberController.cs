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

        // sugeneruojamas random skaicius ir sukuriamas sarasas, 
        // kuriame skaiciuojami neteisingi atsakymai

        public ActionResult Index()
        {
            Number n = new Number();
            n.Generate();
            n.createNotes(new List<string>());
            string answer = n.Answer;
            Session["n"] = n;
            return View();
        }

        // atlikus spejima, tikrinama ar jis atitinka sugeneruota skaiciu,
        // skaiciui atitikus nukreipiama i "win" langa,
        // kitu atveju patiekiama informacija apie neteisinga spejima

        [HttpPost]
        public ActionResult Index(Number model)
        {
            Number n = (Number)Session["n"];
            n.Guess = model.Guess;
            if (n.compareValues())
            {
                //anti-cheat'as kad i Win screena nebutu pereinama per url
                Random r = new Random();
                int randomNumber = r.Next(123, 321);
                Session["random"] = randomNumber;
                return RedirectToAction("Win", new { random = randomNumber });
            }
            else
            {
                return View(n);
            }
        }

        // Nurodoma is kiek bandymu atspeta bei leidziama rezultata patalpinti i duombaze
        public ActionResult Win(int? random)
        {
            //anticheat'o patikra, sukciai nukreipiami i home/index
            if (random != null && random == (int)Session["random"])
            {
                Number n = (Number)Session["n"];
                return View(n);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        // veiksmas rezultatui i duombaze patalpinti (tik uzsiregistravusiems) 
        // + is Number/Win View'so ateina random skaicius patikrinimui ar ne per url patenkama
        [Authorize]
        public ActionResult Insert(int? random)
        {
            // vel tikrinama ar nemeginta patekti per url
            if (random != null && random == (int)Session["random"])
            {
                Number n = (Number)Session["n"];
                var db = new ApplicationDbContext();
                var highscore = new Highscore { Nickname = User.Identity.GetNickname(), Score = n.Notes.Count(), ApplicationUserId = User.Identity.GetUserId() };
                db.Highscores.Add(highscore);
                db.SaveChanges();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}