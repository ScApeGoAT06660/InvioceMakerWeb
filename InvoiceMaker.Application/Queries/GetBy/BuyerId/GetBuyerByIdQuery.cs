using InvoiceMaker.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Queries.GetBy.BuyerId
{
    public class GetBuyerByIdQuery : IRequest<BuyerDto>
    {
        public int Id { get; set; }
        public GetBuyerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
