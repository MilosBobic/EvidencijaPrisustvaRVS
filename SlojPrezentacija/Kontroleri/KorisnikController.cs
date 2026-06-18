using Microsoft.AspNetCore.Mvc;
using SlojPodataka.Modeli;
using SlojPodataka.Repozitorijumi;

namespace SlojPrezentacija.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly RepozitorijumKorisnika repo;

        public KorisnikController(
            RepozitorijumKorisnika repo)
        {
            this.repo = repo;
        }

        private bool JeAdmin()
        {
            return HttpContext.Session
                .GetString("Uloga") == "ADMIN";
        }

        public IActionResult Index()
        {
            if (!JeAdmin())
                return Unauthorized();

            return View(repo.DajSve());
        }

        public IActionResult Dodaj()
        {
            if (!JeAdmin())
                return Unauthorized();

            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(Korisnik k)
        {
            if (!JeAdmin())
                return Unauthorized();

            if (!ModelState.IsValid)
                return View(k);

            repo.Dodaj(k);

            return RedirectToAction("Index");
        }

        public IActionResult Obrisi(int id)
        {
            if (!JeAdmin())
                return Unauthorized();

            return View(repo.DajPoID(id));
        }

        [HttpPost]
        public IActionResult Obrisi(Korisnik k)
        {
            if (!JeAdmin())
                return Unauthorized();

            repo.Obrisi(k.Id);

            return RedirectToAction("Index");
        }
    }
}