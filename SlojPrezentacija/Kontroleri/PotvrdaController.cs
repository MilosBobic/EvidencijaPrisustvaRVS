using Microsoft.AspNetCore.Mvc;
using SlojPodataka.Repozitorijumi;
using SlojPoslovni;
using SlojPoslovni.Modeli;
using SlojPrezentacija.Modeli;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SlojPrezentacija.Kontroleri
{
    public class PotvrdaController : Controller
    {
        private readonly RepozitorijumUcenika repoUcenik;
        private readonly RepozitorijumPredmeta repoPredmet;
        private readonly RepozitorijumCas repoCas;
        private readonly RepozitorijumPrisustva repoPrisustva;

        public PotvrdaController(
            RepozitorijumUcenika repoUcenik,
            RepozitorijumPredmeta repoPredmet,
            RepozitorijumCas repoCas,
            RepozitorijumPrisustva repoPrisustva)
        {
            this.repoUcenik = repoUcenik;
            this.repoPredmet = repoPredmet;
            this.repoCas = repoCas;
            this.repoPrisustva = repoPrisustva;
        }

        public IActionResult Generisi(int ucenikId, int predmetId)
        {
            var ucenik = repoUcenik.DajPoID(ucenikId);
            var predmet = repoPredmet.DajPoId(predmetId);

            var sviCasovi = repoCas.DajSve()
                .Where(x => x.PredmetId == predmetId)
                .ToList();

            var prisustva = repoPrisustva
                .DajPoUcenikuIPredmetu(ucenikId, predmetId);

            int pohadjano = prisustva.Count(x => x.Prisutan);

            int ukupno = sviCasovi.Count;

            double procenat = ukupno > 0
                ? (double)pohadjano / ukupno
                : 0;

            var model = new PotvrdaModel
            {
                BrojPotvrde = Guid.NewGuid().ToString().Substring(0, 8),

                Ucenik = $"{ucenik.Ime} {ucenik.Prezime}",
                
                PredmetId = predmet.Id,

                Predmet = predmet.Naziv,

                UkupnoCasova = ukupno,

                PohadjanoCasova = pohadjano,

                ProcenatPrisustva = procenat,

                Nastavnik = predmet.Korisnik?.KorisnickoIme ?? ""
            };

            return View("Potvrda", model);
        }

        public async Task<IActionResult> ProveriPrisustvo(int ucenikId, int predmetId)
        {
            var client = new HttpClient();

            var url = $"http://localhost:57338/api/KontrolerEvidencije/proveri-prisustvo?ucenikId={ucenikId}&predmetId={predmetId}";

            var json = await client.GetStringAsync(url);

            var obj = JsonSerializer.Deserialize<PotvrdaRezultat>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return View("Rezultat", obj);
        }
    }
}