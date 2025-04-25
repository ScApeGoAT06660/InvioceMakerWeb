using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceMaker.Application.Commands.EditInvoice
{
    public class EditInvoiceCommand : InvoiceDto, IRequest
    {
        public string? SelectedPaymentOption { get; set; }
        public string? SelectedPaymentDeadline { get; set; }

        public IEnumerable<SelectListItem>? PaymentOptionsList { get; set; }
        public IEnumerable<SelectListItem>? DeadlineOptionsList { get; set; }
    }
}
