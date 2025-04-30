using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Queries.GetInvoiceNumber
{
    public class GetNewInvoiceNumberQuery : IRequest<string>
    {
        public string UserId { get; set; }
    }
}
