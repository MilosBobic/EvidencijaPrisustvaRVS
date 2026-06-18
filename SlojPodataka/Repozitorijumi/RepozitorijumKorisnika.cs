using SlojPodataka.Modeli;
using Microsoft.EntityFrameworkCore;

namespace SlojPodataka.Repozitorijumi
{
    public class RepozitorijumKorisnika
    {
        private readonly KontekstBaze db;

        public RepozitorijumKorisnika(
            KontekstBaze db)
        {
            this.db = db;
        }

        public List<Korisnik> DajSve()
        {
            return db.Korisnici.ToList();
        }

        public Korisnik DajPoID(int id)
        {
            return db.Korisnici
                .FirstOrDefault(x => x.Id == id);
        }

        public void Dodaj(Korisnik k)
        {
            db.Korisnici.Add(k);
            db.SaveChanges();
        }

        public void Obrisi(int id)
        {
            var k = DajPoID(id);

            if (k != null)
            {
                db.Korisnici.Remove(k);
                db.SaveChanges();
            }
        }

        public Korisnik Prijavi(
            string korisnickoIme,
            string lozinka)
        {
            return db.Korisnici.FirstOrDefault(
                x => x.KorisnickoIme == korisnickoIme
                  && x.Lozinka == lozinka);
        }
    }
}