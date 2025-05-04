using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InvoiceMaker.Application.Commands.GeneratePDFInvoice
{
    public class GeneratePdfInvoiceCommand : IRequest<byte[]>
    {
        public GeneratePdfInvoiceCommand(int invoiceId)
        {
            InvoiceId = invoiceId;
        }

        public int InvoiceId { get; set; }
    }
}
