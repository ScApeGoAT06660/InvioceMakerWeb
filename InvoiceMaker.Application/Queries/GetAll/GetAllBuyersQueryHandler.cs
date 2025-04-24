using AutoMapper;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Queries.GetAll
{
    internal class GetAllBuyersQueryHandler : IRequestHandler<GetAllBuyersQuery, IEnumerable<BuyerDto>>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;

        public GetAllBuyersQueryHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BuyerDto>> Handle(GetAllBuyersQuery request, CancellationToken cancellationToken)
        {
            var buyer = await _invoiceMakerRepository.GetAllBuyers();
            var dtos = _mapper.Map<IEnumerable<BuyerDto>>(buyer);
            return dtos;
        }
    }
}
