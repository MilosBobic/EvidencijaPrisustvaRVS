using System.ComponentModel.DataAnnotations;

namespace SlojPodataka.Modeli
{
    public class Cas
    {
        public int Id { get; set; }

        [Required]
        public DateTime DatumVreme { get; set; }

        public int PredmetId { get; set; }

        public virtual Predmet? Predmet { get; set; }

        public virtual ICollection<Prisustvo>? Prisustva { get; set; }
    }
}