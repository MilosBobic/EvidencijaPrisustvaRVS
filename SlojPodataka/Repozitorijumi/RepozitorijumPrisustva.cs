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
                .ThenInclude(c => c.Predmet)
                .ToList();
        }

        public List<Prisustvo> DajPoCasu(int casId)
        {
            return db.Prisustva
                .Include(x => x.Ucenik)
                .Include(x => x.Cas)
                .Where(x => x.CasId == casId)
                .ToList();
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

        public void Obrisi(int ucenikId, int casId)
        {
            var prisustvo = db.Prisustva
                .FirstOrDefault(x => x.UcenikId == ucenikId && x.CasId == casId);

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
        public void DodajVise(
            List<Prisustvo> prisustva)
        {
            db.Prisustva.AddRange(prisustva);
            db.SaveChanges();
        }

        public void SacuvajIliAzuriraj(List<Prisustvo> lista)
        {
            foreach (var item in lista)
            {
                var postojece = db.Prisustva
                    .FirstOrDefault(x => x.CasId == item.CasId
                                      && x.UcenikId == item.UcenikId);

                if (postojece == null)
                {
                    db.Prisustva.Add(item);
                }
                else
                {
                    postojece.Prisutan = item.Prisutan;
                }
            }

            db.SaveChanges();
        }

    }
}