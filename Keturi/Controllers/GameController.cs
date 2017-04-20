using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Keturi.Models;

namespace Keturi.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Play(Number model)
        {
            if (model.Answer == null)
            {
                model.Generate();
                model.createNotes(new List<string>());
            }
            if (model.compareValues())
            {
                return RedirectToAction("Win");
            }
            return View(model);
        }
        

        // GET: Game/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
