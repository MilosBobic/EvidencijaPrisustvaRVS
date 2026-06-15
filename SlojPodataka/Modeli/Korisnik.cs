using System.ComponentModel.DataAnnotations;

namespace SlojPodataka.Modeli
{
    public class Korisnik
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string KorisnickoIme { get; set; }

        [Required]
        [StringLength(100)]
        public string Lozinka { get; set; }

        [Required]
        public string Uloga { get; set; } //admin ili korisnik
    }
}