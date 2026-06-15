using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka.Modeli;
using SlojPodataka.Repozitorijumi;

namespace SlojPrezentacija.Kontroleri
{
    public class CasController : Controller
    {
        private readonly RepozitorijumCas repoCas;
        private readonly RepozitorijumPredmeta repoPredmet;

        public CasController(
            RepozitorijumCas repoCas,
            RepozitorijumPredmeta repoPredmet)
        {
            this.repoCas = repoCas;
            this.repoPredmet = repoPredmet;
        }

        public IActionResult Index()
        {
            return View(repoCas.DajSve());
        }

        public IActionResult Dodaj()
        {
            ViewBag.Predmeti =
                new SelectList(
                    repoPredmet.DajSve(),
                    "Id",
                    "Naziv");

            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(Cas c)
        {
            repoCas.Dodaj(c);

            return RedirectToAction("Index");
        }
    }
}