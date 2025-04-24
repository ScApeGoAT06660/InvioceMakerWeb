using InvoiceMaker.Domain.Interfaces;
using InvoiceMaker.Domain;
using MediatR;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Application.Commands.Create;
using InvoiceMaker.Application.Commands.CreateBuyers;
using InvoiceMaker.Application.Commands.CreateItems;
using InvoiceMaker.Application.Commands.CreateSeller;
using Microsoft.AspNetCore.Components.Forms;

namespace InvoiceMaker.Application.Commands.CreateFullInvoice
{
    internal class CreateFullInvoiceCommandHandler : IRequestHandler<CreateFullInvoiceCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMediator _mediator;

        public CreateFullInvoiceCommandHandler(IMediator mediator, IInvoiceMakerRepository invoiceMakerRepository)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateFullInvoiceCommand request, CancellationToken cancellationToken)
        {
            var seller = await _mediator.Send(new CreateSellerCommand
            {
                Name = request.SellerDto.Name,
                VATID = request.SellerDto.VATID,
                StreetAndNo = request.SellerDto.StreetAndNo,
                Postcode = request.SellerDto.Postcode,
                City = request.SellerDto.City
            });

            var buyer = await _mediator.Send(new CreateBuyerCommand
            {
                Name = request.BuyerDto.Name,
                VATID = request.BuyerDto.VATID,
                StreetAndNo = request.BuyerDto.StreetAndNo,
                Postcode = request.BuyerDto.Postcode,
                City = request.BuyerDto.City
            });

            var sellerId = await _invoiceMakerRepository.GetSellerByVATID(request.SellerDto.VATID);
            request.InvoiceDto.SellerId = sellerId.Id;

            request.InvoiceDto.BuyerId = buyer.Id;

            var invoiceId = await _mediator.Send(new CreateInvoiceCommand
            {
                Number = request.InvoiceDto.Number,
                IssueDate = request.InvoiceDto.IssueDate,
                SaleDate = request.InvoiceDto.SaleDate,
                Place = request.InvoiceDto.Place,
                SellerId = request.InvoiceDto.SellerId,
                BuyerId = request.InvoiceDto.BuyerId,
                PaymentType = request.InvoiceDto.PaymentType,
                PaymentDeadline = request.InvoiceDto.PaymentDeadline,
                SellerSignature = request.InvoiceDto.SellerSignature,
                BuyerSignature = request.InvoiceDto.BuyerSignature,
                Notes = request.InvoiceDto.Notes
            });

            var createdInvoice = await _invoiceMakerRepository.GetInvoiceByNumber(request.InvoiceDto.Number);

            foreach (var item in request.ItemsDto)
            {
                item.InvoiceId = createdInvoice.Id;
                await _mediator.Send(new CreateItemCommand
                {
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Position = item.Position,
                    Unit = item.Unit,
                    VAT = item.VAT,
                    Netto = item.Netto,
                    Brutto = item.Brutto,
                    InvoiceId = item.InvoiceId
                });
            }

            return Unit.Value;
        }
    }
}
