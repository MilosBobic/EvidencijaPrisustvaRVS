using Microsoft.AspNetCore.Mvc;
using SlojPodataka.Modeli;
using SlojPodataka.Repozitorijumi;

namespace SlojPrezentacija.Kontroleri
{
    public class UcenikController : Controller
    {
        private readonly RepozitorijumUcenika repo;

        public UcenikController(RepozitorijumUcenika repo)
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
            return View(repo.DajSve());
        }

        public IActionResult Dodaj()
        {
            if (!JeAdmin())
                return Unauthorized();

            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(Ucenik u)
        {
            if (!ModelState.IsValid)
                return View(u);

            repo.Dodaj(u);

            return RedirectToAction("Index");
        }

        public IActionResult Izmeni(int id)
        {
            if (!JeAdmin())
                return Unauthorized();

            return View(repo.DajPoID(id));
        }

        [HttpPost]
        public IActionResult Izmeni(Ucenik u)
        {
            repo.Izmeni(u);

            return RedirectToAction("Index");
        }

        public IActionResult Obrisi(int id)
        {
            if (!JeAdmin())
                return Unauthorized();

            return View(repo.DajPoID(id));
        }

        [HttpPost]
        public IActionResult Obrisi(Ucenik u)
        {
            repo.Obrisi(u.Id);

            return RedirectToAction("Index");
        }
    }
}