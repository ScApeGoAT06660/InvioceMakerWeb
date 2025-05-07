using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain.Interfaces;
using MediatR;
using AutoMapper;
using InvoiceMaker.Application.User;
using InvoiceMaker.Domain.Entities;

namespace InvoiceMaker.Application.Commands.Create
{
    internal class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateInvoiceCommandHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper, IUserContext userContext)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
            _userContext = userContext;
        }


        public async Task<Unit> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = _mapper.Map<Invoice>(request);
            invoice.CreatedById = _userContext.GetCurrentUser().Id; 
            await _invoiceMakerRepository.CreateInvoice(invoice);

            return Unit.Value;
        }
    }
}
