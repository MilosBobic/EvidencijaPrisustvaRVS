using Microsoft.EntityFrameworkCore;
using SlojPodataka.Modeli;

namespace SlojPodataka.Repozitorijumi
{
    public class RepozitorijumCas
    {
        private readonly KontekstBaze db;

        public RepozitorijumCas(KontekstBaze db)
        {
            this.db = db;
        }

        public List<Cas> DajSve()
        {
            return db.Casovi
                .Include(x => x.Predmet)
                .ThenInclude(p => p.Korisnik)
                .ToList();
        }

        public Cas DajPoId(int id)
        {
            return db.Casovi
                .Include(x => x.Predmet)
                .ThenInclude(p => p.Korisnik)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Dodaj(Cas cas)
        {
            db.Casovi.Add(cas);
            db.SaveChanges();
        }

        public void Izmeni(Cas cas)
        {
            db.Casovi.Update(cas);
            db.SaveChanges();
        }

        public void Obrisi(int id)
        {
            var cas = DajPoId(id);

            if (cas != null)
            {
                db.Casovi.Remove(cas);
                db.SaveChanges();
            }
        }
    }
}