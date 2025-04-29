using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.Queries;
using InvoiceMaker.Domain;
using InvoiceMaker.Domain.Interfaces;
using MediatR;


namespace InvoiceMaker.Application.Commands.CreateBuyers
{
    public class CreateBuyerCommandHandler : IRequestHandler<CreateBuyerCommand, BuyerDto>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;

        public CreateBuyerCommandHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
        }

        public async Task<BuyerDto> Handle(CreateBuyerCommand request, CancellationToken cancellationToken)
        {
            var buyer = _mapper.Map<Buyer>(request);
            await _invoiceMakerRepository.CreateBuyer(buyer);

            var result = _mapper.Map<BuyerDto>(buyer);
            result.Id = buyer.Id;

            return result;
        }
    }
}
