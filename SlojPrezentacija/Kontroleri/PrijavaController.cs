using Microsoft.AspNetCore.Mvc;
using SlojPodataka.Repozitorijumi;
using SlojPrezentacija.Modeli;
using System.ComponentModel.DataAnnotations;

namespace SlojPrezentacija.Kontroleri
{
    public class PrijavaController : Controller
    {
        private readonly RepozitorijumKorisnika repo;

        public PrijavaController(
            RepozitorijumKorisnika repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(PrijavaModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var korisnik = repo.Prijavi(
                model.KorisnickoIme,
                model.Lozinka);

            if (korisnik == null)
            {
                ViewBag.Greska =
                    "Pogrešno korisničko ime ili lozinka.";

                return View(model);
            }

            HttpContext.Session.SetInt32(
                "KorisnikId",
                korisnik.Id);

            HttpContext.Session.SetString(
                "Korisnik",
                korisnik.KorisnickoIme);

            HttpContext.Session.SetString(
                "Uloga",
                korisnik.Uloga);

            return RedirectToAction(
                "Index",
                "Pocetna");
        }

        public IActionResult Odjava()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }
}