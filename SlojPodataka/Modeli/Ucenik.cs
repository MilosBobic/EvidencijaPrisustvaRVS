using System.ComponentModel.DataAnnotations;

namespace SlojPodataka.Modeli
{
    public class Ucenik
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(50)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(50)]
        public string Prezime { get; set; }

        public virtual ICollection<Prisustvo> Prisustva { get; set; }
            = new List<Prisustvo>();
    }
}