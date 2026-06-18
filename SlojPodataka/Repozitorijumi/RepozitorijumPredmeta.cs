using Microsoft.EntityFrameworkCore;
using SlojPodataka.Modeli;

namespace SlojPodataka.Repozitorijumi
{
    public class RepozitorijumPredmeta
    {
        private readonly KontekstBaze db;

        public RepozitorijumPredmeta(KontekstBaze db)
        {
            this.db = db;
        }

        public List<Predmet> DajSve()
        {
            return db.Predmeti
                .Include(p => p.Korisnik)
                .ToList();
        }

        public List<Predmet> DajSveSaKorisnikom()
        {
            return db.Predmeti
                .Include(x => x.Korisnik)
                .ToList();
        }

        public Predmet DajPoId(int id)
        {
            return db.Predmeti
                .Include(p => p.Korisnik)
                .Include(p => p.Casovi)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Dodaj(Predmet predmet)
        {
            db.Predmeti.Add(predmet);
            db.SaveChanges();
        }

        public void Izmeni(Predmet predmet)
        {
            db.Predmeti.Update(predmet);
            db.SaveChanges();
        }

        public void Obrisi(int id)
        {
            var predmet = DajPoId(id);

            if (predmet != null)
            {
                db.Predmeti.Remove(predmet);
                db.SaveChanges();
            }
        }
    }
}