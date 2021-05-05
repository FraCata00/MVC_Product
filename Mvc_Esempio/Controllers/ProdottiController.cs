using Mvc_Esempio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mvc_Esempio.Controllers
{
    public class ProdottiController : Controller
    {

        //accesso al database
        private Cloud_MyShopEntities db = new Cloud_MyShopEntities();

        // GET: Prodotti
        public ActionResult Index()
        {
            //elenco dei prodotti 
            var elenco = db.Prodotto.ToList();
            return View(elenco);
        }

        //GET: Prodotti/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Prodotti/Create
        public ActionResult InserisciProdotto(Prodotto prodotto)
        {
            if(!ModelState.IsValid)
                return HttpNotFound();

            if(prodotto == null)
                return RedirectToAction("Errore");

            //esito positivo
            db.Prodotto.Add(prodotto);
            db.SaveChanges();
            return RedirectToAction("PaginaOk");
            
            //esito negativo
        }

        //GET: Prodotti/Delete
        public ActionResult Delete(int id)
        {
            var prodotto = db.Prodotto.Find(id);

            if (prodotto == null)
                return RedirectToAction("Errore");

            return View(prodotto);
        }

        //POST:Prodotti/Delete
        public ActionResult EliminaProdotto(int id)
        {
            var prodotto = db.Prodotto.Find(id);

            if (prodotto == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            db.Prodotto.Remove(prodotto);
            db.SaveChanges();

            return RedirectToAction("Index"); //ritorno all'elenco dei prodotti
        }

        public ActionResult PaginaOk()
        {
            ViewBag.Messaggio = "Operazione eseguita con successo!";
            return View();
        }

        public ActionResult Errore()
        {
            ViewBag.Messaggio = "Operazione fallita!";
            return View();
        }
    }
}