using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain;
using InvoiceMaker.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Commands
{
    internal class EditInvoiceCommandHandler : IRequestHandler<EditInvoiceCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;

        public EditInvoiceCommandHandler(IInvoiceMakerRepository invoiceMakerRepository)
 
        {
            _invoiceMakerRepository = invoiceMakerRepository;
        }

        public async Task<Unit> Handle(EditInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceMakerRepository.GetInvoiceById(request.Id);

            invoice.IssueDate = request.IssueDate;
            invoice.SaleDate = request.SaleDate;
            invoice.Place = request.Place;
            invoice.SellerId = request.SellerId;
            invoice.BuyerId = request.BuyerId;
            invoice.PaymentType = request.PaymentType;
            invoice.PaymentDeadline = request.PaymentDeadline;
            invoice.SellerSignature = request.SellerSignature;
            invoice.BuyerSignature = request.BuyerSignature;
            invoice.Notes = request.Notes;

            invoice.Items.Clear();

            foreach (var itemDto in request.Items ?? Enumerable.Empty<ItemDto>())
            {
                var item = new Item
                {
                    Name = itemDto.Name,
                    Position = itemDto.Position,
                    Quantity = itemDto.Quantity,
                    Unit = itemDto.Unit,
                    VAT = itemDto.VAT,
                    Netto = itemDto.Netto,
                    Brutto = itemDto.Brutto,
                    InvoiceId = invoice.Id
                };

                invoice.Items.Add(item);
            }

            await _invoiceMakerRepository.Commit();

            return Unit.Value;
        }
    }
}
