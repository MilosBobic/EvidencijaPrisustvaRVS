using Microsoft.AspNetCore.Mvc;
using SlojPodataka.Modeli;
using SlojPodataka.Repozitorijumi;

namespace SlojPrezentacija.Kontroleri
{
    public class PredmetController : Controller
    {
        private readonly RepozitorijumPredmeta repo;

        public PredmetController(
            RepozitorijumPredmeta repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View(repo.DajSve());
        }

        public IActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(Predmet p)
        {
            repo.Dodaj(p);

            return RedirectToAction("Index");
        }

        public IActionResult Zavrsi(int id)
        {
            var p = repo.DajPoID(id);

            p.Zavrsen = true;

            repo.Izmeni(p);

            return RedirectToAction("Index");
        }
    }
}