using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka.Modeli;
using SlojPodataka.Repozitorijumi;

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

    public IActionResult Index(int? predmetId)
    {
        var uloga = HttpContext.Session.GetString("Uloga");

        IQueryable<Cas> casovi = repoCas.DajSve().AsQueryable();

        if (uloga != "ADMIN")
        {
            var korisnikId = HttpContext.Session.GetInt32("KorisnikId");

            if (korisnikId == null)
                return RedirectToAction("Index", "Prijava");

            casovi = casovi.Where(c => c.Predmet.KorisnikId == korisnikId);
        }

        if (predmetId.HasValue)
        {
            casovi = casovi.Where(c => c.PredmetId == predmetId.Value);
        }

        ViewBag.PredmetId = predmetId;

        return View(casovi.ToList());
    }

    public IActionResult Detalji(int id)
    {
        var cas = repoCas.DajPoId(id);

        if (cas == null)
            return NotFound();

        return View(cas);
    }

    public IActionResult Dodaj()
    {
        ViewBag.Predmeti = new SelectList(
            repoPredmet.DajSve(),
            "Id",
            "Naziv");

        return View();
    }

    [HttpPost]
    public IActionResult Dodaj(Cas cas)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Predmeti = new SelectList(
                repoPredmet.DajSve(),
                "Id",
                "Naziv");

            return View(cas);
        }

        var predmet = repoPredmet.DajPoId(cas.PredmetId);

        if (predmet == null)
        {
            ModelState.AddModelError("", "Predmet ne postoji");

            ViewBag.Predmeti = new SelectList(
                repoPredmet.DajSve(),
                "Id",
                "Naziv");

            return View(cas);
        }

        int brojPostojecihCasova = repoCas.DajSve()
            .Count(x => x.PredmetId == cas.PredmetId);

        if (brojPostojecihCasova >= predmet.UkupnoBrojCasova)
        {
            ModelState.AddModelError("",
                "Ne može se dodati novi čas. Dostignut je maksimalan broj časova za ovaj predmet.");

            ViewBag.Predmeti = new SelectList(
                repoPredmet.DajSve(),
                "Id",
                "Naziv");

            return View(cas);
        }

        repoCas.Dodaj(cas);

        return RedirectToAction(nameof(Index));
    }

}