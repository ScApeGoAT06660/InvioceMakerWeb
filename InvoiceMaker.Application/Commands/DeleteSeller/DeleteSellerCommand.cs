using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Application.Dto;
using MediatR;

namespace InvoiceMaker.Application.Commands.DeleteSeller
{
    public class DeleteSellerCommand : IRequest
    {
        public int Id { get; set; }
    }
}
