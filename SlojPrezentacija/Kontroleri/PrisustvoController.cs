using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka.Modeli;
using SlojPodataka.Repozitorijumi;
using SlojPrezentacija.Modeli;

namespace SlojPrezentacija.Kontroleri
{
    public class PrisustvoController : Controller
    {
        private readonly RepozitorijumPrisustva repo;
        private readonly RepozitorijumUcenika repoUcenik;
        private readonly RepozitorijumCas repoCas;
        private readonly RepozitorijumPredmeta repoPredmet;
        private readonly RepozitorijumUcenikPredmet repoUP;

        public PrisustvoController(
            RepozitorijumPrisustva repo,
            RepozitorijumUcenika repoUcenik,
            RepozitorijumCas repoCas,
            RepozitorijumPredmeta repoPredmet,
            RepozitorijumUcenikPredmet repoUP)
        {
            this.repo = repo;
            this.repoUcenik = repoUcenik;
            this.repoCas = repoCas;
            this.repoPredmet = repoPredmet;
            this.repoUP = repoUP;
        }

        public IActionResult Index()
        {
            var uloga = HttpContext.Session.GetString("Uloga");

            if (uloga == "ADMIN")
                return View(repoCas.DajSve());

            var korisnikId = HttpContext.Session.GetInt32("KorisnikId");

            if (korisnikId == null)
                return RedirectToAction("Index", "Prijava");

            var casovi = repoCas.DajSve()
                .Where(c => c.Predmet.KorisnikId == korisnikId)
                .ToList();

            return View(casovi);
        }


        public IActionResult Dodaj(int casId)
        {
            var cas = repoCas.DajPoId(casId);
            if (cas == null)
                return NotFound();

            var postojece = repo.DajSve()
                .Where(x => x.CasId == casId)
                .ToList();

            var ucenici = repoUP.DajUcenikeZaPredmet(cas.PredmetId);

            var model = new EvidencijaPrisustvaModeli
            {
                CasId = casId,
                Ucenici = ucenici.Select(x => new UcenikPrisustvoModel
                {
                    UcenikId = x.Id,
                    ImePrezime = x.Ime + " " + x.Prezime,
                    Prisutan = postojece.Any(p => p.UcenikId == x.Id && p.Prisutan)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Dodaj(EvidencijaPrisustvaModeli model)
        {
            Console.WriteLine("CAS ID: " + model.CasId);
            Console.WriteLine("UCENICI NULL? " + (model.Ucenici == null));

            var lista = model.Ucenici.Select(x => new Prisustvo
            {
                CasId = model.CasId,
                UcenikId = x.UcenikId,
                Prisutan = x.Prisutan
            }).ToList();

            repo.SacuvajIliAzuriraj(lista);

            return RedirectToAction("Evidencija", new { casId = model.CasId });
        }

        public IActionResult Evidencija(int casId)
        {
            if (casId <= 0)
                return BadRequest("CasId nije validan");

            var cas = repoCas.DajPoId(casId);

            if (cas == null)
                return NotFound();

            var data = repo.DajSve()
                .Where(x => x.CasId == casId)
                .ToList();

            ViewBag.CasId = casId;

            return View(data);
        }

        public IActionResult Provera()
        {
            var model = new ProveraPrisustvaModel();

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
        public IActionResult Provera(
            ProveraPrisustvaModel model)
        {
            var prisustva =
                repo.DajPoUcenikuIPredmetu(
                    model.UcenikId,
                    model.PredmetId);

            int ukupno = prisustva.Count;

            int prisutan =
                prisustva.Count(x => x.Prisutan);

            double procenat =
                ukupno == 0
                    ? 0
                    : (double)prisutan / ukupno * 100;

            ViewBag.Rezultat =
                $"Prisustvo: {procenat:F2}%";

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