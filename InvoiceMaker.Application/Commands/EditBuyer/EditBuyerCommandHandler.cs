using AutoMapper;
using InvoiceMaker.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Commands.EditBuyer
{
    internal class EditBuyerCommandHandler : IRequestHandler<EditBuyerCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;

        public EditBuyerCommandHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditBuyerCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _invoiceMakerRepository.GetBuyerByID(request.Id);
            _mapper.Map(request, buyer);
            await _invoiceMakerRepository.Commit();
            return Unit.Value;
        }
    }
}
