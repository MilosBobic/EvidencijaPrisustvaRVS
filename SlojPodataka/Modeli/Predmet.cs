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

        public bool Zavrsen { get; set; } = false;

        public virtual ICollection<Cas>? Casovi { get; set; }
    }
}