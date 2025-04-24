using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain;

namespace InvoiceMaker.Application.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> CreateInvoice(InvoiceDto invoice);
        Task<Seller> CreateSeller(SellerDto seller);
        Task<Buyer> CreateBuyer(BuyerDto buyerDto);
        Task CreateItem(ItemDto item);
        Task<IEnumerable<InvoiceDto>> GetAllInvoices();
        Task<IEnumerable<SellerDto>> GetAllSellers();
        Task<IEnumerable<BuyerDto>> GetAllBuyers();
    }
}