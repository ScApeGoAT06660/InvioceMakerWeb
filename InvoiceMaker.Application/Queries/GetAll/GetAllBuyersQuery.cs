using InvoiceMaker.Application.Dto;
using MediatR;


namespace InvoiceMaker.Application.Queries.GetAll
{
    public class GetAllBuyersQuery : IRequest<IEnumerable<BuyerDto>>
    {
    }
}
