namespace SlojPrezentacija.Modeli
{
    public class PotvrdaModel
    {
        public string BrojPotvrde { get; set; }

        public string Ucenik { get; set; }

        public string Predmet { get; set; }

        public int UkupnoCasova { get; set; }

        public int PohadjanoCasova { get; set; }

        public double ProcenatPrisustva { get; set; }

        public string Nastavnik { get; set; }

        public int PredmetId { get; set; }
    }
}