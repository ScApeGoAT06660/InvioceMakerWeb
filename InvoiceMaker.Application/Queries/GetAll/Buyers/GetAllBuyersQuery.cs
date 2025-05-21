using InvoiceMaker.Application.Dto;
using MediatR;


namespace InvoiceMaker.Application.Queries.GetAll.Buyers
{
    public class GetAllBuyersQuery : IRequest<IEnumerable<BuyerDto>>
    {
    }
}
