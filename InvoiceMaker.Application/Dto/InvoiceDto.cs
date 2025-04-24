using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Dto
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string? Number { get; set; } = default!;
        public DateTime IssueDate { get; set; }
        public DateTime SaleDate { get; set; }
        public string? Place { get; set; }

        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public string? BuyerType { get; set; }

        public List<ItemDto>? Items { get; set; }

        public string? PaymentType { get; set; }
        public string? PaymentDeadline { get; set; }

        public string? SellerSignature { get; set; }
        public string? BuyerSignature { get; set; }

        public string? Notes { get; set; }

        public SellerDto? Seller { get; set; }
        public BuyerDto? Buyer { get; set; }
        public bool IsEditable { get; set; }
    }
}
