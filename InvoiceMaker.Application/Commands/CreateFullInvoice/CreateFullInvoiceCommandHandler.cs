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
        private readonly IMapper _mapper;

        public CreateFullInvoiceCommandHandler(IMediator mediator, IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateFullInvoiceCommand request, CancellationToken cancellationToken)
        {
            var checkSeller = await _invoiceMakerRepository.GetSellerByVATID(request.SellerDto.VATID);

            if (checkSeller == null)
            {
                var sellerCommand = _mapper.Map<CreateSellerCommand>(request.SellerDto);
                await _mediator.Send(sellerCommand);

                var sellerId = await _invoiceMakerRepository.GetSellerByVATID(request.SellerDto.VATID);
                request.InvoiceDto.SellerId = sellerId.Id;
            }
            else
            {
                request.InvoiceDto.SellerId = checkSeller.Id;
            }

            var checkBuyer = await _invoiceMakerRepository.GetBuyerByVATID(request.BuyerDto.VATID);

            if (checkBuyer == null) 
            {
                var buyerCommand = _mapper.Map<CreateBuyerCommand>(request.BuyerDto);
                await _mediator.Send(buyerCommand);

                var buyerId = await _invoiceMakerRepository.GetBuyerByVATID(request.BuyerDto.VATID);
                request.InvoiceDto.BuyerId = buyerId.Id;
            }
            else
            {
                request.InvoiceDto.BuyerId = checkBuyer.Id;
            }


            var invoiceId = _mapper.Map<CreateInvoiceCommand>(request.InvoiceDto);
            await _mediator.Send(invoiceId);

            var createdInvoice = await _invoiceMakerRepository.GetInvoiceByNumber(request.InvoiceDto.Number);

            foreach (var item in request.ItemsDto)
            {
                item.InvoiceId = createdInvoice.Id;
                var itemCommand = _mapper.Map<CreateItemCommand>(item);
                await _mediator.Send(itemCommand);
            }

            return Unit.Value;
        }
    }
}
