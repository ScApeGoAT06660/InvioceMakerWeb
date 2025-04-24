using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InvoiceMaker.Application.Dto;

namespace InvoiceMaker.Application.Commands.CreateItems
{
    public class ItemDtoValidator : AbstractValidator<CreateItemCommand>
    {
        public ItemDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Podaj nazwę pozycji.");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Podaj ilość.");

            RuleFor(x => x.VAT)
                .NotEmpty()
                .WithMessage("Podaj VAT.");

            RuleFor(x => x.Netto)
                .NotEmpty()
                .WithMessage("Podaj netto.");

            RuleFor(x => x.Brutto)
                .NotEmpty()
                .WithMessage("Podaj brutto.");
        }

    }
}
