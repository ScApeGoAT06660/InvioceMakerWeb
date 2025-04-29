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
        [Display(Name = "Nazwa firmy")]
        public string Name { get; set; } = default!;
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
