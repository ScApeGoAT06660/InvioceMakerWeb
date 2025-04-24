using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Application.Dto;
using MediatR;

namespace InvoiceMaker.Application.Commands.CreateSeller
{
    public class CreateSellerCommand : SellerDto, IRequest
    {
    }
}
