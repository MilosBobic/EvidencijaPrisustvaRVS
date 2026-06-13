using Microsoft.AspNetCore.Mvc;
using SlojPrezentacija.Repozitorijumi;

namespace SlojPrezentacija.Kontroleri
{
    public class UcenikController : Controller
    {
        public IActionResult Index(string pretraga)
        {
            var lista = repo.DajSve();

            if (!string.IsNullOrEmpty(pretraga))
            {
                lista = lista
                    .Where(x =>
                        x.Ime.Contains(pretraga) ||
                        x.Prezime.Contains(pretraga))
                    .ToList();
            }

            return View(lista);
        }
    }
}
