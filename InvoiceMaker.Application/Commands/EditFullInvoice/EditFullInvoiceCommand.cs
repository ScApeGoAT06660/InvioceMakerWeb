using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceMaker.Application.Commands.EditFullInvoice
{
    public class EditFullInvoiceCommand : IRequest
    {
        public SellerDto SellerDto { get; set; }
        public BuyerDto BuyerDto { get; set; }
        public InvoiceDto InvoiceDto { get; set; }
        public List<ItemDto> ItemsDto { get; set; }
        [Display(Name = "Płatność")]
        public string SelectedPaymentOption { get; set; }

        [BindNever]
        public List<SelectListItem> PaymentOptionsList { get; set; }
        [Display(Name = "Termin płatności")]
        public string SelectedPaymentDeadline { get; set; }
        [BindNever]
        public List<SelectListItem> DeadlineOptionsList { get; set; }
        [BindNever]
        public List<SellerDto> Sellers { get; set; } = new();
        [BindNever]
        public List<BuyerDto> Buyers { get; set; } = new();

    }
}
