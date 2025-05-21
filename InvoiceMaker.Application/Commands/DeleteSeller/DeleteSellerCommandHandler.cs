using AutoMapper;
using InvoiceMaker.Domain;
using InvoiceMaker.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Commands.DeleteSeller
{
    internal class DeleteSellerCommandHandler : IRequestHandler<DeleteSellerCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;

        public DeleteSellerCommandHandler(IInvoiceMakerRepository invoiceMakerRepository)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
        }

        public async Task<Unit> Handle(DeleteSellerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _invoiceMakerRepository.DeleteSeller(request.Id);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Wystąpił błąd podczas usuwania sprzedawcy.", ex);
            }
        }
    }
}
