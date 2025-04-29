using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Commands.DeleteBuyer
{
    public class DeleteBuyerCommand : IRequest
    {
        public int Id { get; set; }
    }
}
