using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SlojPrezentacija.Modeli
{
    public class ProveraPrisustvaModel
    {
        [Required]
        public int UcenikId { get; set; }

        [Required]
        public int PredmetId { get; set; }

        public List<SelectListItem>? Ucenici { get; set; }

        public List<SelectListItem>? Predmeti { get; set; }
    }
}