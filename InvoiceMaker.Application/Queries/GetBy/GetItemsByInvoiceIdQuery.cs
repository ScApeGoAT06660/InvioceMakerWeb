using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain;
using MediatR;

namespace InvoiceMaker.Application.Queries.GetBy
{
    public class GetItemsByInvoiceIdQuery : IRequest<List<ItemDto>>
    {
        public int InvoiceId { get; set; }

        public GetItemsByInvoiceIdQuery(int invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}
