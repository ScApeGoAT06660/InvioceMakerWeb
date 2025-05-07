using InvoiceMaker.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Queries.GetBy
{
    public class GetBuyerByNipQuery : IRequest<BuyerDto>
    {
        public string Nip { get; set; }

        public GetBuyerByNipQuery(string nip)
        {
            Nip = nip;
        }
    }
}
