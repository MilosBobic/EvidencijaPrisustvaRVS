using Microsoft.AspNetCore.Mvc;

namespace SlojPrezentacija.Controllers
{
    public class PotvrdaKontroler : Controller
    {
        private readonly HttpClient client;

        public PotvrdaKontroler(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IActionResult> Index()
        {
            string rezultat =
                await client.GetStringAsync(
                "https://localhost:7000/api/evidencija/proveri-prisustvo");

            ViewBag.Rezultat = rezultat;

            return View();
        }
    }
}