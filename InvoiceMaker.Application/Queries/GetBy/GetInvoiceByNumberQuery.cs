using InvoiceMaker.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Queries.GetBy
{
    public class GetInvoiceByNumberQuery : IRequest<InvoiceDto>
    {
        public int Id { get; set; }

        public GetInvoiceByNumberQuery(int id)
        {
            Id = id;
        }
    }
}
