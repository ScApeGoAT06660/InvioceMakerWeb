using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommand : IRequest
    {
        public int Id { get; set; }
    }
}
