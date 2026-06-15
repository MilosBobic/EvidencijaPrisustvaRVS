using Microsoft.AspNetCore.Mvc;
using SlojPodataka.Modeli;

namespace SlojPrezentacija.Kontroleri
{
    public class KorisnikController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session
                .GetString("Uloga") != "ADMIN")
            {
                return Unauthorized();
            }

            return View();
        }
    }
}