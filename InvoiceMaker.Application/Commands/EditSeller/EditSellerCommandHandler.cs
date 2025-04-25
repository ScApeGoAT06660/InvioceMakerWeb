using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain;
using InvoiceMaker.Domain.Interfaces;
using MediatR;

namespace InvoiceMaker.Application.Commands.EditSeller
{
    public class EditSellerCommandHandler : IRequestHandler<EditSellerCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;

        public EditSellerCommandHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditSellerCommand request, CancellationToken cancellationToken)
        {
            var seller = await _invoiceMakerRepository.GetSellerByID(request.Id);

            _mapper.Map(request, seller);

            await _invoiceMakerRepository.Commit();

            return Unit.Value;
        }
    }
}
