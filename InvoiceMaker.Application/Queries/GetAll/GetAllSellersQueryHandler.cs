using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain.Interfaces;
using MediatR;

namespace InvoiceMaker.Application.Queries.GetAll
{
    internal class GetAllSellersQueryHandler : IRequestHandler<GetAllSellersQuery, IEnumerable<SellerDto>>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;

        public GetAllSellersQueryHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SellerDto>> Handle(GetAllSellersQuery request, CancellationToken cancellationToken)
        {
            var seller = await _invoiceMakerRepository.GetAllSellers();
            var dtos = _mapper.Map<IEnumerable<SellerDto>>(seller);
            return dtos;
        }
    }
}
