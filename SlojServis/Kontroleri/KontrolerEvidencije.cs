using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlojPodataka.Repozitorijumi;
using SlojPoslovni;
using SlojPoslovni.Modeli;
using System.Runtime.InteropServices;

namespace SlojServis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KontrolerEvidencije : ControllerBase
    {
        private readonly RepozitorijumPrisustva repoPrisustva;
        private readonly RepozitorijumCas repoCas;
        private readonly IWebHostEnvironment env;

        public KontrolerEvidencije(
            RepozitorijumPrisustva repoPrisustva,
            RepozitorijumCas repoCas,
            IWebHostEnvironment env)
        {
            this.repoPrisustva = repoPrisustva;
            this.repoCas = repoCas;
            this.env = env;
        }

        [HttpGet("proveeri-prisustvo")]
        public IActionResult ProveeriPrisustvo()
        {
            try
            {
                string putanja =
                    Path.Combine(env.ContentRootPath,
                    "parametri.json");

                string json =
                    System.IO.File.ReadAllText(putanja);

                var parametar =
                    JsonConvert.DeserializeObject<ParametarPrisustva>(json);

                var proces =
                    new ProcesIzdavanjaPotvrde(
                        repoPrisustva,
                        repoCas);

                string rezultat =
                    proces.Obradi(parametar);

                return Ok(rezultat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}