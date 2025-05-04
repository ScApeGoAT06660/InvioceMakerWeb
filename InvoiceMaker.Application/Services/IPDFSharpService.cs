using InvoiceMaker.Domain;

namespace InvoiceMaker.Application.Services
{
    public interface IPDFSharpService
    {
        byte[] GenerateInvoicePdf(Invoice invoice, Seller seller, Buyer buyer);
    }
}