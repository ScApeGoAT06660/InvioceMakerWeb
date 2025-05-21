using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Number { get; set; } = default!;
        public DateTime IssueDate { get; set; }
        public DateTime SaleDate { get; set; }
        public string? Place { get; set; }
        public int SellerId { get; set; } //ReturnSellerId
        public int BuyerId { get; set; } //ReturnBuyerId
        public string? BuyerType { get; set; } //ReturnBuyerType
        public virtual List<Item>? Items { get; set; } //ReturnItemList
        public string? PaymentType { get; set; }
        public string? PaymentDeadline { get; set; }
        public string? SellerSignature { get; set; }
        public string? BuyerSignature { get; set; }
        public string? Notes { get; set; }
        public virtual Seller? Seller { get; set; }
        public virtual Buyer? Buyer { get; set; }
        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }

    }
}
