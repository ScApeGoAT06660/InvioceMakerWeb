using System.ComponentModel.DataAnnotations;

namespace InvoiceMaker.Application.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; } = default!;
        public string? Position { get; set; }
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }
        [Display(Name = "Jednostka")]
        public string? Unit { get; set; }
        public string? VAT { get; set; }
        public decimal Netto { get; set; }
        public decimal Brutto { get; set; }
        public int InvoiceId { get; set; }
    }
}
