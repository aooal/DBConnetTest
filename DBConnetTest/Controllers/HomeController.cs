using DBConnetTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBConnetTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DBmanager dBmanager = new DBmanager();
            List<Card> cards = null;
            
            if (string.IsNullOrEmpty(Request.Form["KeyWorld"]))
            {
                cards = dBmanager.GetCards();
            }
            else
            {
                cards = dBmanager.QueryByKeyWorld(Request.Form["KeyWorld"]);
            }
            
            ViewBag.cards = cards;
            return View();
        }

        public ActionResult CreateCard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCard(Card card)
        {
            DBmanager dBmanager = new DBmanager();
            try
            {
                dBmanager.NewCard(card);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult EditCard(int id)
        {
            DBmanager dBmanager = new DBmanager();
            Card card = dBmanager.GetCardById(id);
            return View(card);
        }
        [HttpPost]
        public ActionResult EditCard(Card card)
        {
            DBmanager dBmanager = new DBmanager();
            dBmanager.UpdateCard(card);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCard(int id)
        {
            DBmanager dBmanager = new DBmanager();
            dBmanager.DeleteCardById(id);
            return RedirectToAction("Index");
        }
    }
}