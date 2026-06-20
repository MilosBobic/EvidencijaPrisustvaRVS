using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SlojPrezentacija.Kontroleri
{
    public class PocetnaController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session
                .GetString("Korisnik") == null)
            {
                return RedirectToAction(
                    "Index",
                    "Prijava");
            }

            return View();
        }
    }
}