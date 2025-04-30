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
using InvoiceMaker.Application.User;

namespace InvoiceMaker.Application.Commands.CreateSeller
{
    public class CreateSellerCommandHandler : IRequestHandler<CreateSellerCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateSellerCommandHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper, IUserContext userContext)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
        {
            var seller = _mapper.Map<Seller>(request);
            seller.CreatedById = _userContext.GetCurrentUser().Id;
            await _invoiceMakerRepository.CreateSeller(seller);
            return Unit.Value;
        }
    }
}
