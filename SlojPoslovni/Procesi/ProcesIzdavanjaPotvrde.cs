using SlojPodataka.Repozitorijumi;
using SlojPoslovni.Modeli;
using System.Linq;

namespace SlojPoslovni
{
    public class ProcesIzdavanjaPotvrde
    {
        private readonly RepozitorijumPrisustva repoPrisustva;
        private readonly RepozitorijumCas repoCas;

        public ProcesIzdavanjaPotvrde(
            RepozitorijumPrisustva repoPrisustva,
            RepozitorijumCas repoCas)
        {
            this.repoPrisustva = repoPrisustva;
            this.repoCas = repoCas;
        }

        public string Obradi(ParametarPrisustva parametar)
        {
            var sviCasovi = repoCas
                .DajSve()
                .Where(x => x.PredmetId == parametar.PredmetId)
                .ToList();

            if (sviCasovi.Count == 0)
                return "Nema evidentiranih časova.";

            var prisustva =
                repoPrisustva.DajPoUcenikuIPredmetu(
                    parametar.UcenikId,
                    parametar.PredmetId);

            int brojPrisutnih =
                prisustva.Count(x => x.Prisutan);

            double procenat =
                (double)brojPrisutnih / sviCasovi.Count;

            if (procenat > parametar.PragPrisustva)
            {
                return $"Potvrda se izdaje. Prisustvo: {procenat:P0}";
            }

            return $"Potvrda se NE izdaje. Prisustvo: {procenat:P0}";
        }
    }
}