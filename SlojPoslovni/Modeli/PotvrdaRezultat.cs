namespace SlojPoslovni.Modeli
{
    public class PotvrdaRezultat
    {
        public int ucenikId { get; set; }
        public int predmetId { get; set; }
        public double Procenat { get; set; }
        public bool IspunjavaUslov { get; set; }
    }
}