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
    public class GetInvoiceByNumberQueryHandler : IRequestHandler<GetInvoiceByNumberQuery, InvoiceDto>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;

        public GetInvoiceByNumberQueryHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
        }

        public async Task<InvoiceDto> Handle(GetInvoiceByNumberQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceMakerRepository.GetInvoiceById(request.Id);
            var dto = _mapper.Map<InvoiceDto>(invoice);

            return dto;
        }
    }
}
