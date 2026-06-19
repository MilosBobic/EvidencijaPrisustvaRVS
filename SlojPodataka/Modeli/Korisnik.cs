using System.ComponentModel.DataAnnotations;

namespace SlojPodataka.Modeli
{
    public class Korisnik
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Unesite korisničko ime.")]
        [RegularExpression(
            @"^[a-zA-Z0-9]{4,20}$",
            ErrorMessage = "Korisničko ime mora sadržati 4-20 slova i brojeva bez razmaka.")]
        [StringLength(20)]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Unesite lozinku.")]
        [RegularExpression(
            @"^(?=.*[A-Z])(?=.*\d).{6,}$",
            ErrorMessage = "Lozinka mora imati najmanje 6 karaktera, jedno veliko slovo i jedan broj.")]
        [StringLength(100)]
        public string Lozinka { get; set; }

        [Required]
        public string Uloga { get; set; } //admin ili korisnik

        public ICollection<Predmet> Predmeti { get; set; }
            = new List<Predmet>();
    }
}