using InvoiceMaker.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Commands.EditBuyer
{
    public class EditBuyerCommand : BuyerDto, IRequest
    {
    }
}
