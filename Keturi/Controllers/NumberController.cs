using Keturi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keturi.Controllers
{
    public class NumberController : Controller
    {
        // GET: Number
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

        [HttpPost]
        public ActionResult Index(string guess)
        {

            Number n = (Number)Session["n"];
            if (n.compareValues(guess, n.Answer))
            {
                return RedirectToAction("Win");
            }
            else
            {
                ViewBag.list = n.Notes;
                return View();
            }

        }

        public ActionResult Win()
        {
            Number n = (Number)Session["n"];
            ViewBag.count = n.Notes.Count();
            return View();
        }
    }
}