using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlojPoslovni;
using SlojPoslovni.Modeli;

namespace SlojServis.Kontroleri
{
    [ApiController]
    [Route("api/[controller]")]
    public class KontrolerEvidencije : ControllerBase
    {
        private readonly IEvidencijaServis servis;
        private readonly IWebHostEnvironment env;

        public KontrolerEvidencije(
            IEvidencijaServis servis,
            IWebHostEnvironment env)
        {
            this.servis = servis;
            this.env = env;
        }

        [HttpGet("proveri-prisustvo")]
        public IActionResult ProveriPrisustvo(int ucenikId, int predmetId)
        {
            try
            {
                string putanja = Path.Combine(env.ContentRootPath, "parametri.json");
                var json = System.IO.File.ReadAllText(putanja);

                var cfg = JsonConvert.DeserializeObject<ParametarPrisustva>(json);

                if (cfg == null)
                    return StatusCode(500, "Neispravan parametri.json");

                var rezultat = servis.Obradi(
                    ucenikId,
                    predmetId,
                    cfg.PragPrisustva
                );

                return Ok(rezultat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}