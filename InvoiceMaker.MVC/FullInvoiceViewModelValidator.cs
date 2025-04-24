using FluentValidation;
using InvoiceMaker.Application.Commands.Create;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.MVC.Models;

namespace InvoiceMaker.MVC
{
    public class FullInvoiceViewModelValidator : AbstractValidator<CreateFullInvoiceCommand>
    {
        public FullInvoiceViewModelValidator(
            IValidator<SellerDto> sellerValidator,
            IValidator<BuyerDto> buyerValidator,
            IValidator<InvoiceDto> invoiceValidator,
            IValidator<ItemDto> itemValidator)
        {
            RuleFor(x => x.SellerDto)
                .SetValidator(sellerValidator);

            RuleFor(x => x.BuyerDto)
                .SetValidator(buyerValidator);

            RuleFor(x => x.InvoiceDto)
                .SetValidator(invoiceValidator);

            RuleForEach(x => x.ItemsDto)
                .SetValidator(itemValidator);
        }
    }
}
