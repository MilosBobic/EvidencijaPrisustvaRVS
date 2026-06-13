namespace SlojPodataka.Modeli
{
    public class Prisustvo
    {
        public int Id { get; set; }

        public int UcenikId { get; set; }

        public virtual Ucenik Ucenik { get; set; }

        public int CasId { get; set; }

        public virtual Cas Cas { get; set; }

        public bool Prisutan { get; set; }
    }
}