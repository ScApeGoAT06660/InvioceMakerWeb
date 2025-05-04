using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain;

namespace InvoiceMaker.Application.Services
{
    public interface IMRiFService
    {
        Task<BuyerDto> TakeTraderInfo(string nip);
    }
}