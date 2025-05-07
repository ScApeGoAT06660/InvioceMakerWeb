using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Application;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.Services;
using MediatR;


namespace InvoiceMaker.Application.Queries.GetBy
{
    public class GetBuyerByNipQueryHandler : IRequestHandler<GetBuyerByNipQuery, BuyerDto>
    {
        private readonly IMRiFService _mrif;

        public GetBuyerByNipQueryHandler(IMRiFService mrif)
        {
            _mrif = mrif;
        }

        public async Task<BuyerDto> Handle(GetBuyerByNipQuery request, CancellationToken cancellationToken)
        {
            var buyer = await _mrif.TakeTraderInfo(request.Nip);
            return buyer;
        }
    }
}
