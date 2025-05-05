using AutoMapper;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Queries.GetBy
{
    public class GetItemsByInvoiceIdQueryHandler : IRequestHandler<GetItemsByInvoiceIdQuery, List<ItemDto>>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;

        public GetItemsByInvoiceIdQueryHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
        }

        public async Task<List<ItemDto>> Handle(GetItemsByInvoiceIdQuery request, CancellationToken cancellationToken)
        {
            var items = await _invoiceMakerRepository.GetItemsByInvoiceId(request.InvoiceId);
            return _mapper.Map<List<ItemDto>>(items);
        }
    }
}
