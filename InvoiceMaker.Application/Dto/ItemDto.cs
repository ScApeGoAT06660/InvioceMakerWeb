namespace InvoiceMaker.Application.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Position { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public string? VAT { get; set; }
        public decimal Netto { get; set; }
        public decimal Brutto { get; set; }
        public int InvoiceId { get; set; }
    }
}
