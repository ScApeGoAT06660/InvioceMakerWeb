using System.ComponentModel.DataAnnotations;

namespace InvoiceMaker.Application.Dto
{
    public class ItemDto
    {
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa nie może przekraczać 100 znaków")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; } = default!;

        [StringLength(100, ErrorMessage = "Pozycja nie może przekraczać 100 znaków")]
        [Display(Name = "Pozycja")]
        public string? Position { get; set; }

        [Required(ErrorMessage = "Ilość jest wymagana")]
        [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być większa niż 0")]
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }

        [StringLength(20, ErrorMessage = "Jednostka nie może przekraczać 20 znaków")]
        [Display(Name = "Jednostka")]
        public string? Unit { get; set; }

        [Display(Name = "VAT")]
        public string? VAT { get; set; }

        [Required(ErrorMessage = "Wartość netto jest wymagana")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Netto musi być większe niż 0")]
        [Display(Name = "Cena netto")]
        public decimal Netto { get; set; }

        [Required(ErrorMessage = "Wartość brutto jest wymagana")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Brutto musi być większe niż 0")]
        [Display(Name = "Cena brutto")]
        public decimal Brutto { get; set; }

        [Required]
        public int InvoiceId { get; set; }
    }
}
