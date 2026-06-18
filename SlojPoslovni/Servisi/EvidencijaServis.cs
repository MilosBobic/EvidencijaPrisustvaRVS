using SlojPodataka.Modeli;
using SlojPodataka.Repozitorijumi;
using SlojPoslovni.Modeli;

public class EvidencijaServis : IEvidencijaServis
{
    private readonly RepozitorijumUcenikPredmet repoUP;
    private readonly RepozitorijumCas repoCas;
    private readonly RepozitorijumPrisustva repoPrisustva;

    public EvidencijaServis(
        RepozitorijumUcenikPredmet repoUP,
        RepozitorijumCas repoCas,
        RepozitorijumPrisustva repoPrisustva)
    {
        this.repoUP = repoUP;
        this.repoCas = repoCas;
        this.repoPrisustva = repoPrisustva;
    }

    public PotvrdaRezultat Obradi(int ucenikId, int predmetId, double pragPrisustva)
    {
        var upis = repoUP.DajSve()
            .FirstOrDefault(x => x.UcenikId == ucenikId && x.PredmetId == predmetId);

        if (upis == null)
            throw new Exception("Upis ne postoji");

        var predmet = upis.Predmet;

        var sviCasovi = repoCas.DajSve()
            .Where(x => x.PredmetId == predmetId)
            .ToList();

        var prisustva = repoPrisustva
            .DajPoUcenikuIPredmetu(ucenikId, predmetId)
            ?? new List<Prisustvo>();

        int prisutni = prisustva.Count(x => x.Prisutan);
        int ukupno = predmet.UkupnoBrojCasova;

        double procenat = ukupno == 0 ? 0 : (double)prisutni / ukupno;

        return new PotvrdaRezultat
        {
            ucenikId = ucenikId,
            predmetId = predmetId,
            Procenat = procenat,
            IspunjavaUslov = procenat >= pragPrisustva
        };
    }
}