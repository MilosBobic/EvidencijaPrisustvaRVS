using Microsoft.EntityFrameworkCore;
using SlojPodataka.Modeli;

namespace SlojPodataka.Repozitorijumi
{
    public class RepozitorijumPrisustva
    {
        private readonly KontekstBaze db;

        public RepozitorijumPrisustva(KontekstBaze db)
        {
            this.db = db;
        }

        public List<Prisustvo> DajSve()
        {
            return db.Prisustva
                .Include(x => x.Ucenik)
                .Include(x => x.Cas)
                .ThenInclude(x => x.Predmet)
                .ToList();
        }

        public Prisustvo DajPoId(int id)
        {
            return db.Prisustva
                .Include(x => x.Ucenik)
                .Include(x => x.Cas)
                .ThenInclude(x => x.Predmet)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Dodaj(Prisustvo prisustvo)
        {
            db.Prisustva.Add(prisustvo);
            db.SaveChanges();
        }

        public void Izmeni(Prisustvo prisustvo)
        {
            db.Prisustva.Update(prisustvo);
            db.SaveChanges();
        }

        public void Obrisi(int id)
        {
            var prisustvo = DajPoId(id);

            if (prisustvo != null)
            {
                db.Prisustva.Remove(prisustvo);
                db.SaveChanges();
            }
        }

        public List<Prisustvo> DajPoUcenikuIPredmetu(
            int ucenikId,
            int predmetId)
        {
            return db.Prisustva
                .Include(x => x.Cas)
                .Where(x =>
                    x.UcenikId == ucenikId &&
                    x.Cas.PredmetId == predmetId)
                .ToList();
        }
    }
}