using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InvoiceMaker.Application.Dto;

namespace InvoiceMaker.Application.Commands.Create
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            RuleFor(x => x.Number)
               .NotEmpty()
               .WithMessage("XYZ");

            RuleFor(x => x.IssueDate)
                .NotEmpty()
                .WithMessage("Podaj datę wystawienia.");

            RuleFor(x => x.SaleDate)
                .NotEmpty()
                .WithMessage("Podaj datę sprzedaży.");
        }
    }
}
