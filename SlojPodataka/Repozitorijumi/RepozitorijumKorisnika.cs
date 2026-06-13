using SlojPodataka.Modeli;
using System.Linq;

namespace SlojPodataka.Repozitorijumi
{
    public class RepozitorijumKorisnika
    {
        private readonly KontekstBaze db;

        public RepozitorijumKorisnika(KontekstBaze db)
        {
            this.db = db;
        }

        public Korisnik Prijavi(string korisnickoIme, string lozinka)
        {
            return db.Korisnici.FirstOrDefault(x =>
                x.KorisnickoIme == korisnickoIme &&
                x.Lozinka == lozinka);
        }
    }
}