using InvoiceMaker.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Commands.Create
{
    public class CreateFullInvoiceCommand : IRequest
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
    }
}
