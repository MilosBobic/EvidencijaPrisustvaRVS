using Microsoft.EntityFrameworkCore;
using SlojPodataka.Modeli;

namespace SlojPodataka.Repozitorijumi
{
    public class RepozitorijumUcenikPredmet
    {
        private readonly KontekstBaze db;

        public RepozitorijumUcenikPredmet(
            KontekstBaze db)
        {
            this.db = db;
        }

        public List<UcenikPredmet> DajSve()
        {
            return db.UceniciPredmeti
                .Include(x => x.Ucenik)
                .Include(x => x.Predmet)
                .ToList();
        }

        public void Dodaj(UcenikPredmet up)
        {
            db.UceniciPredmeti.Add(up);
            db.SaveChanges();
        }

        public List<Ucenik> DajUcenikePredmeta(int predmetId)
        {
            return db.UceniciPredmeti
                .Where(x => x.PredmetId == predmetId)
                .Select(x => x.Ucenik)
                .ToList();
        }

        public List<Ucenik> DajUcenikeZaPredmet(int predmetId)
        {
            return db.UceniciPredmeti
                .Where(x => x.PredmetId == predmetId)
                .Select(x => x.Ucenik)
                .ToList();
        }
    }
}