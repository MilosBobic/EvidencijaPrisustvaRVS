namespace SlojPodataka.Modeli
{
    public class UcenikPredmet
    {
        public int Id { get; set; }

        public int UcenikId { get; set; }

        public Ucenik Ucenik { get; set; }

        public int PredmetId { get; set; }

        public Predmet Predmet { get; set; }
    }
}