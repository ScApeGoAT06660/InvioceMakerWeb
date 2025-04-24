using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using InvoiceMaker.Application.Dto;

namespace InvoiceMaker.MVC.Models
{
    public class FullInvoiceViewModel
    {
        public InvoiceDto InvoiceDto { get; set; } = new();
        public SellerDto SellerDto { get; set; } = new();
        public BuyerDto BuyerDto { get; set; } = new();
        public List<ItemDto> ItemsDto { get; set; } = new();
        public string SelectedPaymentOption { get; set; }
        public List<SelectListItem> PaymentOptionsList { get; set; }
        public string SelectedPaymentDeadline { get; set; }
        public List<SelectListItem> DeadlineOptionsList { get; set; }
    }
}
