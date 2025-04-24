using InvoiceMaker.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Commands.Create
{
    public class CreateInvoiceCommand : InvoiceDto, IRequest
    {

    }
}
