using InvoiceMaker.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;

        public DeleteInvoiceCommandHandler(IInvoiceMakerRepository invoiceMakerRepository)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
        }

        public async Task<Unit> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            await _invoiceMakerRepository.DeleteInvoice(request.Id);
            return Unit.Value;  
        }
    }
}
