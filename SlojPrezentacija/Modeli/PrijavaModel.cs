using System.ComponentModel.DataAnnotations;

namespace SlojPrezentacija.Modeli
{
    public class PrijavaModel
    {
        [Required]
        public string KorisnickoIme { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }
    }
}