using System.ComponentModel.DataAnnotations;

namespace SlojPodataka.Modeli
{
    public class Predmet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv je obavezan")]
        [StringLength(100)]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Broj časova je obavezan")]
        public int UkupnoBrojCasova { get; set; }

        public int? KorisnikId { get; set; }

        public Korisnik? Korisnik { get; set; }

        public bool Zavrsen { get; set; } = false;

        public ICollection<UcenikPredmet> UceniciPredmeti { get; set; }
            = new List<UcenikPredmet>();

        public ICollection<Cas> Casovi { get; set; }
            = new List<Cas>();

    }
}