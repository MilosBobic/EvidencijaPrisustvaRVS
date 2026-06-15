using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka.Modeli;
using SlojPodataka.Repozitorijumi;
using SlojPrezentacija.Modeli;
using System.Net.Http;

namespace SlojPrezentacija.Kontroleri
{
    public class PrisustvoController : Controller
    {
        private readonly RepozitorijumPrisustva repo;
        private readonly RepozitorijumUcenika repoUcenik;
        private readonly RepozitorijumCas repoCas;
        private readonly RepozitorijumPredmeta repoPredmet;
        private readonly IHttpClientFactory clientFactory;

        public PrisustvoController(
            RepozitorijumPrisustva repo,
            RepozitorijumUcenika repoUcenik,
            RepozitorijumCas repoCas,
            RepozitorijumPredmeta repoPredmet,
            IHttpClientFactory clientFactory)
        {
            this.repo = repo;
            this.repoUcenik = repoUcenik;
            this.repoCas = repoCas;
            this.repoPredmet = repoPredmet;
        }

        public IActionResult Index()
        {
            return View(repo.DajSve());
        }

        public IActionResult Dodaj()
        {
            ViewBag.Ucenici =
                new SelectList(
                    repoUcenik.DajSve(),
                    "Id",
                    "Ime");

            ViewBag.Casovi =
                new SelectList(
                    repoCas.DajSve(),
                    "Id",
                    "DatumVreme");

            return View();
        }
        public IActionResult Provera()
        {
            var model = new ProveraPrisustvaVM();

            model.Ucenici = repoUcenik.DajSve()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Ime + " " + x.Prezime
                })
                .ToList();

            model.Predmeti = repoPredmet.DajSve()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Dodaj(Prisustvo p)
        {
            repo.Dodaj(p);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Provera(
        ProveraPrisustvaVM model)
        {
            var client = clientFactory.CreateClient();

            string url =
                $"https://localhost:5001/api/evidencija/proveri-prisustvo" +
                $"?ucenikId={model.UcenikId}" +
                $"&predmetId={model.PredmetId}";

            var odgovor =
                await client.GetStringAsync(url);

            ViewBag.Rezultat = odgovor;

            model.Ucenici = repoUcenik.DajSve()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Ime + " " + x.Prezime
                })
                .ToList();

            model.Predmeti = repoPredmet.DajSve()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                })
                .ToList();

            return View(model);
        }
    }
}