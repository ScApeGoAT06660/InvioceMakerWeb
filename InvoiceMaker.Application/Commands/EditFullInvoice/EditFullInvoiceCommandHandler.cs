using AutoMapper;
using InvoiceMaker.Application.Commands.CreateBuyers;
using InvoiceMaker.Application.Commands.CreateItems;
using InvoiceMaker.Application.Commands.CreateSeller;
using InvoiceMaker.Application.Commands.EditInvoice;
using InvoiceMaker.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Commands.EditFullInvoice
{
    public class EditFullInvoiceCommandHandler : IRequestHandler<EditFullInvoiceCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EditFullInvoiceCommandHandler(IInvoiceMakerRepository invoiceMakerRepository, IMediator mediator, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditFullInvoiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingSeller = await _invoiceMakerRepository.GetSellerByVATID(request.SellerDto.VATID);
                if (existingSeller == null)
                {
                    var sellerCommand = _mapper.Map<CreateSellerCommand>(request.SellerDto);
                    await _mediator.Send(sellerCommand);
                    existingSeller = await _invoiceMakerRepository.GetSellerByVATID(request.SellerDto.VATID);
                }
                request.InvoiceDto.SellerId = existingSeller.Id;

                var existingBuyer = await _invoiceMakerRepository.GetBuyerByVATID(request.BuyerDto.VATID);
                if (existingBuyer == null)
                {
                    var buyerCommand = _mapper.Map<CreateBuyerCommand>(request.BuyerDto);
                    await _mediator.Send(buyerCommand);
                    existingBuyer = await _invoiceMakerRepository.GetBuyerByVATID(request.BuyerDto.VATID);
                }
                request.InvoiceDto.BuyerId = existingBuyer.Id;

                var editInvoiceCommand = _mapper.Map<EditInvoiceCommand>(request.InvoiceDto);
                await _mediator.Send(editInvoiceCommand);

                var invoice = await _invoiceMakerRepository.GetInvoiceByNumber(request.InvoiceDto.Number);

                invoice.Items.Clear();

                foreach (var item in request.ItemsDto)
                {
                    item.InvoiceId = invoice.Id;
                    var itemCommand = _mapper.Map<CreateItemCommand>(item);
                    await _mediator.Send(itemCommand);
                }

                return Unit.Value;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Wystąpił błąd podczas edytowania faktury.", ex);
            }
        }
    }
}
