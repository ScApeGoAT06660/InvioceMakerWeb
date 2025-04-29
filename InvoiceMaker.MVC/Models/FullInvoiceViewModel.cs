using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using InvoiceMaker.Application.Dto;
using System.ComponentModel.DataAnnotations;

namespace InvoiceMaker.MVC.Models
{
    public class FullInvoiceViewModel
    {
        public InvoiceDto InvoiceDto { get; set; } = new();
        public SellerDto SellerDto { get; set; } = new();
        public BuyerDto BuyerDto { get; set; } = new();
        public List<ItemDto> ItemsDto { get; set; } = new();
        [Display(Name = "Rodzaj płatności")]
        public string SelectedPaymentOption { get; set; }
        [Display(Name = "Termin płatności")]
        public List<SelectListItem> PaymentOptionsList { get; set; }
        public string SelectedPaymentDeadline { get; set; }
        public List<SelectListItem> DeadlineOptionsList { get; set; }
    }
}
