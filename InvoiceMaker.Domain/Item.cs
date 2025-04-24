using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Domain
{
    public class Item
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = default!;
        public string? Position { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public string? VAT { get; set; }
        public decimal Netto { get; set; }
        public decimal Brutto { get; set; }
    }
}
