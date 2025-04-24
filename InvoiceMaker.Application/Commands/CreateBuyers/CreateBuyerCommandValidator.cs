using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InvoiceMaker.Application.Dto;

namespace InvoiceMaker.Application.Commands.CreateBuyers
{
    internal class CreateBuyerCommandValidator : AbstractValidator<CreateBuyerCommand>
    {
        public CreateBuyerCommandValidator(IValidator<TraderDto> traderValidator)
        {
            Include(traderValidator);

        }
    }
}
