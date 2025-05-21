using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Application.Services;
using InvoiceMaker.Domain.Interfaces;
using MediatR;

namespace InvoiceMaker.Application.Commands.GeneratePDFInvoice
{
    public class GeneratePdfInvoiceCommandHandler : IRequestHandler<GeneratePdfInvoiceCommand, byte[]>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IPDFSharpService _pdfSharpService;

        public GeneratePdfInvoiceCommandHandler(IInvoiceMakerRepository invoiceMakerRepository, IPDFSharpService pDFSharpService)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _pdfSharpService = pDFSharpService;
        }

        public async Task<byte[]> Handle(GeneratePdfInvoiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var invoice = await _invoiceMakerRepository.GetInvoiceById(request.InvoiceId);
                if (invoice == null)
                    throw new Exception("Faktura nie znaleziona.");

                var seller = await _invoiceMakerRepository.GetSellerByID(invoice.SellerId);
                var buyer = await _invoiceMakerRepository.GetBuyerByID(invoice.BuyerId);

                return _pdfSharpService.GenerateInvoicePdf(invoice, seller, buyer);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Wystąpił błąd podczas generowania faktury.", ex);
            }
        }
    }
}
