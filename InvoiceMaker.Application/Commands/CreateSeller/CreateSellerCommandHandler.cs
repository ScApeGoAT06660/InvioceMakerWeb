using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Domain.Interfaces;
using InvoiceMaker.Domain;
using MediatR;
using AutoMapper;
using InvoiceMaker.Application.Dto;

namespace InvoiceMaker.Application.Commands.CreateSeller
{
    public class CreateSellerCommandHandler : IRequestHandler<CreateSellerCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;

        public CreateSellerCommandHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
        {
            var seller = _mapper.Map<Seller>(request);
            await _invoiceMakerRepository.CreateSeller(seller);
            return Unit.Value;
        }
    }
}
