using InvoiceMaker.Application.Dto;
using MediatR;

namespace InvoiceMaker.Application.Queries.GetAll.Sellers
{
    public class GetAllSellersQuery : IRequest<IEnumerable<SellerDto>>
    {
    }
}
