using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InvoiceMaker.Application.Dto;

namespace InvoiceMaker.Application.Commands.CreateSeller
{
    public class SellerCommandValidator : AbstractValidator<CreateSellerCommand>
    {
        public SellerCommandValidator(IValidator<TraderDto> traderValidator)
        {
            Include(traderValidator);

        }
    }
}
