using AutoMapper;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Queries.GetBy.SellerId
{
    public class GetSellerByIdQueryHandler : IRequestHandler<GetSellerByIdQuery, SellerDto>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;

        public GetSellerByIdQueryHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
        }

        public async Task<SellerDto> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
        {
            var seller = await _invoiceMakerRepository.GetSellerByID(request.Id);
            var dto = _mapper.Map<SellerDto>(seller);

            return dto;
        }
    }
}
