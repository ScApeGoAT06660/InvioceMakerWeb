using InvoiceMaker.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Commands.DeleteBuyer
{
    public class DeleteBuyerCommandHandler : IRequestHandler<DeleteBuyerCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;

        public DeleteBuyerCommandHandler(IInvoiceMakerRepository invoiceMakerRepository)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
        }

        public async Task<Unit> Handle(DeleteBuyerCommand request, CancellationToken cancellationToken)
        {
            await _invoiceMakerRepository.DeleteBuyer(request.Id);
            return Unit.Value;
        }
    }
}
