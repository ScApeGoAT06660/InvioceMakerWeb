using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.Queries;
using InvoiceMaker.Application.User;
using InvoiceMaker.Domain.Entities;
using InvoiceMaker.Domain.Interfaces;
using MediatR;


namespace InvoiceMaker.Application.Commands.CreateBuyers
{
    public class CreateBuyerCommandHandler : IRequestHandler<CreateBuyerCommand, BuyerDto>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateBuyerCommandHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper, IUserContext userContext)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<BuyerDto> Handle(CreateBuyerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var buyer = _mapper.Map<Buyer>(request);
                buyer.CreatedById = _userContext.GetCurrentUser().Id;
                await _invoiceMakerRepository.CreateBuyer(buyer);

                var result = _mapper.Map<BuyerDto>(buyer);
                result.Id = buyer.Id;

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Wystąpił błąd podczas tworzenia nabywcy.", ex);
            }
        }
    }
}
