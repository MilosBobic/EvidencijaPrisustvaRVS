using Microsoft.AspNetCore.Mvc;

namespace SlojPrezentacija.Kontroleri
{
    public class KorisnikController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
