using SlojPoslovni.Modeli;

public interface IEvidencijaServis
{
    PotvrdaRezultat Obradi(int ucenikId, int predmetId, double pragPrisustva);
}