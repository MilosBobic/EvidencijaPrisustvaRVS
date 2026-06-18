using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka.Modeli;
using SlojPodataka.Repozitorijumi;

namespace SlojPrezentacija.Kontroleri
{
    public class PredmetController : Controller
    {
        private readonly RepozitorijumPredmeta repo;
        private readonly RepozitorijumKorisnika repoKorisnik;

        public PredmetController(
            RepozitorijumPredmeta repo,
            RepozitorijumKorisnika repoKorisnik)
        {
            this.repo = repo;
            this.repoKorisnik = repoKorisnik;
        }

        private bool JeAdmin()
        {
            return HttpContext.Session
                .GetString("Uloga") == "ADMIN";
        }

        public IActionResult Index()
        {
            var uloga = HttpContext.Session.GetString("Uloga");

            var predmeti = repo.DajSveSaKorisnikom().AsQueryable();

            if (uloga != "ADMIN")
            {
                var korisnikId = HttpContext.Session.GetInt32("KorisnikId");

                if (korisnikId == null)
                    return RedirectToAction("Index", "Prijava");

                predmeti = predmeti
                    .Where(p => p.KorisnikId == korisnikId);
            }

            return View(predmeti.ToList());
        }


        public IActionResult Dodaj()
        {
            var korisnici = repoKorisnik.DajSve()
                .Where(x => x.Uloga != null &&
                            x.Uloga.ToUpper() == "KORISNIK")
                .ToList();

            ViewBag.Korisnici = new SelectList(korisnici, "Id", "KorisnickoIme");

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
            var p = repo.DajPoId(id);

            p.Zavrsen = true;

            repo.Izmeni(p);

            return RedirectToAction("Index");
        }

        public IActionResult Obrisi(int id)
        {
            if (!JeAdmin())
                return Unauthorized();

            return View(repo.DajPoId(id));
        }

        [HttpPost]
        public IActionResult Obrisi(Predmet p)
        {
            if (!JeAdmin())
                return Unauthorized();

            repo.Obrisi(p.Id);

            return RedirectToAction("Index");
        }

        public IActionResult Detalji(int id)
        {
            var predmet = repo.DajPoId(id);

            if (predmet == null)
                return NotFound();

            return View(predmet);
        }

        public IActionResult Izmeni(int id)
        {
            var predmet = repo.DajPoId(id);

            if (predmet == null)
                return NotFound();

            return View(predmet);
        }

        [HttpPost]
        public IActionResult Izmeni(Predmet predmet)
        {
            repo.Izmeni(predmet);
            return RedirectToAction(nameof(Index));
        }
    }
}