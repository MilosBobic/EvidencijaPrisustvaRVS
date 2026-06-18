namespace SlojPrezentacija.Modeli
{
    public class EvidencijaPrisustvaModeli
    {
        public int CasId { get; set; }

        public List<UcenikPrisustvoModel> Ucenici { get; set; }
            = new();
    }

    public class UcenikPrisustvoModel
    {
        public int UcenikId { get; set; }

        public string ImePrezime { get; set; }

        public bool Prisutan { get; set; }
    }
}