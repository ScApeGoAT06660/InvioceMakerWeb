using AutoMapper;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.User;
using InvoiceMaker.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Queries.GetAll.Buyers
{
    internal class GetAllBuyersQueryHandler : IRequestHandler<GetAllBuyersQuery, IEnumerable<BuyerDto>>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetAllBuyersQueryHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper, IUserContext userContext)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<BuyerDto>> Handle(GetAllBuyersQuery request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser().Id;
            var buyer = await _invoiceMakerRepository.GetAllBuyers(user);
            var dtos = _mapper.Map<IEnumerable<BuyerDto>>(buyer);
            return dtos;
        }
    }
}
