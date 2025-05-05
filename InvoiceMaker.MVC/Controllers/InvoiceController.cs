using AutoMapper;
using InvoiceMaker.Application.Commands.Create;
using InvoiceMaker.Application.Commands.CreateBuyers;
using InvoiceMaker.Application.Commands.CreateSeller;
using InvoiceMaker.Application.Commands.DeleteBuyer;
using InvoiceMaker.Application.Commands.DeleteInvoice;
using InvoiceMaker.Application.Commands.DeleteSeller;
using InvoiceMaker.Application.Commands.EditBuyer;
using InvoiceMaker.Application.Commands.EditFullInvoice;
using InvoiceMaker.Application.Commands.EditInvoice;
using InvoiceMaker.Application.Commands.EditSeller;
using InvoiceMaker.Application.Commands.GeneratePDFInvoice;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.Queries.GetAll;
using InvoiceMaker.Application.Queries.GetBy;
using InvoiceMaker.Application.Queries.GetInvoiceNumber;
using InvoiceMaker.Infrastructure;
using InvoiceMaker.MVC.Extensions;
using InvoiceMaker.MVC.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceMaker.MVC.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IDropdownOptionsProvider _dropdownOptionsProvider;

        public InvoiceController(IMediator mediator, IMapper mapper, IDropdownOptionsProvider dropdownOptionsProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dropdownOptionsProvider = dropdownOptionsProvider;
        }

        public async Task<IActionResult> CreateFullInvoice()
        {
            var newInvoiceNumber = await _mediator.Send(new GetNewInvoiceNumberQuery());
            var sellers = await _mediator.Send(new GetAllSellersQuery());
            var buyers = await _mediator.Send(new GetAllBuyersQuery());

            var command = new CreateFullInvoiceCommand
            {
                InvoiceDto = new InvoiceDto
                {
                    Number = newInvoiceNumber.ToString()
                },
                ItemsDto = new List<ItemDto> { new ItemDto() },
                PaymentOptionsList = _dropdownOptionsProvider.GetPaymentMethods(),
                DeadlineOptionsList = _dropdownOptionsProvider.GetPaymentDeadlines(),
                Sellers = sellers.ToList(),
                Buyers = buyers.ToList()
            };

            if (Request.Cookies.TryGetValue("LastSellerId", out var sellerIdStr)
            && int.TryParse(sellerIdStr, out var sellerId))
            {
                var sellerDto = await _mediator.Send(new GetSellerByIdQuery(sellerId));
                command.SellerDto = sellerDto;
            }

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFullInvoice(CreateFullInvoiceCommand model)
        {
            ModelState.Remove(nameof(model.PaymentOptionsList));
            ModelState.Remove(nameof(model.DeadlineOptionsList));

            if (!ModelState.IsValid)
            {
                model.PaymentOptionsList = _dropdownOptionsProvider.GetPaymentMethods();

                model.DeadlineOptionsList = _dropdownOptionsProvider.GetPaymentDeadlines();

                if (model.ItemsDto == null || model.ItemsDto.Count == 0)
                {
                    model.ItemsDto = new List<ItemDto> { new ItemDto() };
                }

                return View(model);
            }

            model.InvoiceDto.PaymentType = model.SelectedPaymentOption;
            model.InvoiceDto.PaymentDeadline = model.SelectedPaymentDeadline;

            this.SetNotification("success", $"Stoworzono fakturę {model.InvoiceDto.Number}.");

            await _mediator.Send(model);

            if (model.InvoiceDto.SellerId > 0)
            {
                Response.Cookies.Append("LastSellerId", model.InvoiceDto.SellerId.ToString(), new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30),
                    IsEssential = true
                });
            }

            return RedirectToAction(nameof(InvoiceIndex));
        }

        [HttpPost]
        [Route("Invoice/Edit/{id}")]
        public async Task<IActionResult> InvoiceEdit(int id, EditFullInvoiceCommand model)
        {
            model.InvoiceDto.PaymentType = model.SelectedPaymentOption;
            model.InvoiceDto.PaymentDeadline = model.SelectedPaymentDeadline;

            ModelState.Remove(nameof(model.PaymentOptionsList));
            ModelState.Remove(nameof(model.DeadlineOptionsList));

            model.InvoiceDto.Id = id;

            if (!ModelState.IsValid)
            {
                model.PaymentOptionsList = new SelectList(
                    _dropdownOptionsProvider.GetPaymentMethods(), "Value", "Text", model.SelectedPaymentOption
                ).ToList();

                model.DeadlineOptionsList = new SelectList(
                    _dropdownOptionsProvider.GetPaymentDeadlines(), "Value", "Text", model.SelectedPaymentDeadline
                ).ToList();

                if (model.ItemsDto == null || !model.ItemsDto.Any())
                {
                    model.ItemsDto = new List<ItemDto> { new ItemDto() };
                }

                return View(model);

            }
            this.SetNotification("success", $"Zedytowano fakturę {model.InvoiceDto.Number}.");

            await _mediator.Send(model);

            return RedirectToAction(nameof(InvoiceIndex));
        }


        [Route("Invoice/Details/{id}")]
        public async Task<IActionResult> InvoiceDetails(int id)
        {
            var dto = await _mediator.Send(new GetInvoiceByNumberQuery(id));
            return View(dto);
        }

        [Route("Buyer/Details/{id}")]
        public async Task<IActionResult> BuyerDetails(int id)
        {
            var dto = await _mediator.Send(new GetBuyerByIdQuery(id));
            return View(dto);
        }
  
        [Route("Invoice/Edit/{id}")]
        public async Task<IActionResult> InvoiceEdit(int id)
        {
            var invoiceDto = await _mediator.Send(new GetInvoiceByNumberQuery(id));
            var sellerDto = await _mediator.Send(new GetSellerByIdQuery(invoiceDto.SellerId));
            var buyerDto = await _mediator.Send(new GetBuyerByIdQuery(invoiceDto.BuyerId));
            var itemsDto = await _mediator.Send(new GetItemsByInvoiceIdQuery(id));

            var edit = new EditFullInvoiceCommand
            {
                InvoiceDto = invoiceDto,
                SellerDto = sellerDto,
                BuyerDto = buyerDto,
                ItemsDto = itemsDto
            };

            edit.SelectedPaymentOption = edit.InvoiceDto.PaymentType;
            edit.SelectedPaymentDeadline = edit.InvoiceDto.PaymentDeadline;

            edit.PaymentOptionsList = _dropdownOptionsProvider.GetPaymentMethods();

            edit.DeadlineOptionsList = _dropdownOptionsProvider.GetPaymentDeadlines();

            if (edit.ItemsDto == null || !edit.ItemsDto.Any())
            {
                edit.ItemsDto = new List<ItemDto> { new ItemDto() };
            }

            return View(edit);
        }

        [Route("Seller/Edit/{id}")]
        public async Task<IActionResult> SellerEdit(int id)
        {
            var dto = await _mediator.Send(new GetSellerByIdQuery(id));
            EditSellerCommand edit = _mapper.Map<EditSellerCommand>(dto);

            return View(edit);
        }

        [HttpPost]
        [Route("Seller/Edit/{id}")]
        public async Task<IActionResult> SellerEdit(int id, EditSellerCommand model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            this.SetNotification("success", $"Zedytowano sprzedawcę {model.Name}.");

            await _mediator.Send(model);

            return RedirectToAction(nameof(SellerIndex));
        }

        [Route("Buyer/Edit/{id}")]
        public async Task<IActionResult> BuyerEdit(int id)
        {
            var dto = await _mediator.Send(new GetBuyerByIdQuery(id));
            EditBuyerCommand edit = _mapper.Map<EditBuyerCommand>(dto);

            return View(edit);
        }

        [HttpPost]
        [Route("Buyer/Edit/{id}")]
        public async Task<IActionResult> BuyerEdit(int id, EditBuyerCommand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SetNotification("success", $"Zedytowano kontrahenta {model.Name}.");

            await _mediator.Send(model);

            return RedirectToAction(nameof(BuyerIndex));
        }

        [Route("Seller/Details/{id}")]
        public async Task<IActionResult> SellerDetails(int id)
        {
            var dto = await _mediator.Send(new GetSellerByIdQuery(id));
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeller(CreateSellerCommand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SetNotification("success", $"Utworzono sprzedawcę {model.Name}.");

            await _mediator.Send(model);

            return RedirectToAction(nameof(SellerIndex));
        }

        public IActionResult CreateSeller()
        {
            var command = new CreateSellerCommand();
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBuyer(CreateBuyerCommand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SetNotification("success", $"Utworzono kontrahenta {model.Name}.");

            await _mediator.Send(model);

            return RedirectToAction(nameof(BuyerIndex));
        }


        public IActionResult CreateBuyer()
        {
            var command = new CreateBuyerCommand();
            return View(command);
        }

        [HttpPost]
        [Route("Seller/Delete/{id}")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            var command = new DeleteSellerCommand { Id = id };
            this.SetNotification("success", $"Usunięto sprzedawcę.");
            await _mediator.Send(command);
            return RedirectToAction(nameof(SellerIndex));
        }

        [HttpPost]
        [Route("Invoice/Delete/{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var command = new DeleteInvoiceCommand { Id = id };
            this.SetNotification("success", $"Usunięto fakturę.");
            await _mediator.Send(command);
            return RedirectToAction(nameof(InvoiceIndex));
        }

        [HttpPost]
        [Route("Buyer/Delete/{id}")]
        public async Task<IActionResult> DeleteBuyer(int id)
        {
            var command = new DeleteBuyerCommand { Id = id };
            this.SetNotification("success", $"Usunięto kontrahenta.");
            await _mediator.Send(command);
            return RedirectToAction(nameof(BuyerIndex));
        }

        public async Task<IActionResult> InvoiceIndex()
        {
            var invoices = await _mediator.Send(new GetAllInvoicesQuery());
            return View(invoices);
        }

        public async Task<IActionResult> SellerIndex()
        {
            var seller = await _mediator.Send(new GetAllSellersQuery());
            return View(seller);
        }

        public async Task<IActionResult> BuyerIndex()
        {
            var buyer = await _mediator.Send(new GetAllBuyersQuery());
            return View(buyer);
        }

        [HttpGet]
        public async Task<IActionResult> GetBuyerByVatNumber(string nip)
        {
            var buyer = await _mediator.Send(new GetBuyerByNipQuery(nip));

            if (buyer == null)
                return NotFound("Nie znaleziono kontrahenta");

            return Json(buyer);
        }

        [HttpGet("Invoice/Download/{id}")]
        public async Task<IActionResult> DownloadPDF(int id)
        {
            var pdfBytes = await _mediator.Send(new GeneratePdfInvoiceCommand(id));
            var fileName = $"Faktura_{id}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}
