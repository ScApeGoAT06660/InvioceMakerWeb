using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Dto
{
    public class TraderDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa firmy jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa firmy nie może przekraczać 100 znaków")]
        [Display(Name = "Nazwa firmy")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "NIP jest wymagany")]
        [StringLength(20, ErrorMessage = "NIP nie może przekraczać 20 znaków")]
        [Display(Name = "NIP")]
        public string VATID { get; set; } = default!;
        [Display(Name = "Ulica")]
        public string? StreetAndNo { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string? Postcode { get; set; }
        [Display(Name = "Miasto")]
        public string? City { get; set; }
    }
}
