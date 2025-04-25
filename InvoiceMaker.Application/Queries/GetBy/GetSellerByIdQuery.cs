using InvoiceMaker.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Queries.GetBy
{
    public class GetSellerByIdQuery : IRequest<SellerDto>
    {
        public int Id { get; set; }

        public GetSellerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
