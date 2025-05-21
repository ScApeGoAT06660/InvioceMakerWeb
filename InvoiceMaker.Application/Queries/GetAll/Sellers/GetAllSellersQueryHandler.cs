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

namespace InvoiceMaker.Application.Queries.GetAll.Sellers
{
    internal class GetAllSellersQueryHandler : IRequestHandler<GetAllSellersQuery, IEnumerable<SellerDto>>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetAllSellersQueryHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper, IUserContext userContext)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<SellerDto>> Handle(GetAllSellersQuery request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser().Id;
            var seller = await _invoiceMakerRepository.GetAllSellers(user);
            var dtos = _mapper.Map<IEnumerable<SellerDto>>(seller);
            return dtos;
        }
    }
}
