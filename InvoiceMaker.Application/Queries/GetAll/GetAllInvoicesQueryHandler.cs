using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.User;
using InvoiceMaker.Domain.Interfaces;
using MediatR;

namespace InvoiceMaker.Application.Queries.GetAll
{
    internal class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceDto>>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetAllInvoicesQueryHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper, IUserContext userContext)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var invoices = await _invoiceMakerRepository.GetAllInvoices(user.Id);
            var dtos = _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
            return dtos;
        }
    }
}
