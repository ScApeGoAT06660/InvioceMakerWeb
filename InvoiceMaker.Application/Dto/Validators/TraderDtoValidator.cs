using FluentValidation;
using InvoiceMaker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Dto.Validators
{
    public class TraderDtoValidator : AbstractValidator<TraderDto>
    {
        public TraderDtoValidator(IInvoiceMakerRepository repository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Podaj nazwę firmy.");

            RuleFor(x => x.VATID)
                .NotEmpty()
                .MaximumLength(10)
                .WithMessage("Podaj numer NIP.")
                .Custom((value, context) =>
                {
                    var exsistingVATID = repository.GetByVATID(value).Result;
                    if (exsistingVATID != null)
                    {
                        context.AddFailure("Podany kontrahent już istnieje.");
                    }
                });
        }
    }
}
