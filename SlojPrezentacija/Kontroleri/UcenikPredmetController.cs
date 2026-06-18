using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka.Modeli;
using SlojPodataka.Repozitorijumi;

namespace SlojPrezentacija.Kontroleri
{
    public class UcenikPredmetController : Controller
    {
        private readonly RepozitorijumUcenikPredmet repo;
        private readonly RepozitorijumUcenika repoUcenik;
        private readonly RepozitorijumPredmeta repoPredmet;

        public UcenikPredmetController(
            RepozitorijumUcenikPredmet repo,
            RepozitorijumUcenika repoUcenik,
            RepozitorijumPredmeta repoPredmet)
        {
            this.repo = repo;
            this.repoUcenik = repoUcenik;
            this.repoPredmet = repoPredmet;
        }

        public IActionResult Index(int? predmetId)
        {
            var model = repo.DajSve();

            if (predmetId.HasValue)
            {
                model = model
                    .Where(x => x.PredmetId == predmetId)
                    .ToList();
            }

            ViewBag.PredmetId = predmetId;

            return View(model);
        }

        public IActionResult Dodaj(int? predmetId)
        {
            ViewBag.Ucenici = new SelectList(
                repoUcenik.DajSve(),
                "Id",
                "Ime");

            ViewBag.Predmeti = new SelectList(
                repoPredmet.DajSve(),
                "Id",
                "Naziv");

            return View(new UcenikPredmet
            {
                PredmetId = predmetId ?? 0
            });
        }

        [HttpPost]
        public IActionResult Dodaj(UcenikPredmet up)
        {
            repo.Dodaj(up);
            return RedirectToAction("Index");
        }
    }
}