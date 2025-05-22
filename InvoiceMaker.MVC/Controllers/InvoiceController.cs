using AutoMapper;
using InvoiceMaker.Application.Commands.Create;
using InvoiceMaker.Application.Commands.DeleteInvoice;
using InvoiceMaker.Application.Commands.EditFullInvoice;
using InvoiceMaker.Application.Commands.GeneratePDFInvoice;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.Queries.GetAll.Buyers;
using InvoiceMaker.Application.Queries.GetAll.Invoices;
using InvoiceMaker.Application.Queries.GetAll.Sellers;
using InvoiceMaker.Application.Queries.GetBy;
using InvoiceMaker.Application.Queries.GetBy.BuyerId;
using InvoiceMaker.Application.Queries.GetBy.ItemByInvoiceId;
using InvoiceMaker.Application.Queries.GetBy.SellerId;
using InvoiceMaker.Application.Queries.GetInvoiceNumber;
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

        public async Task<IActionResult> Create()
        {
            try
            {
                var newInvoiceNumber = await _mediator.Send(new GetNewInvoiceNumberQuery());
                var sellers = await _mediator.Send(new GetAllSellersQuery());
                var buyers = await _mediator.Send(new GetAllBuyersQuery());

                var command = new CreateFullInvoiceCommand
                {
                    InvoiceDto = new InvoiceDto { Number = newInvoiceNumber.ToString() },
                    ItemsDto = new List<ItemDto> { new ItemDto() },
                    PaymentOptionsList = _dropdownOptionsProvider.GetPaymentMethods(),
                    DeadlineOptionsList = _dropdownOptionsProvider.GetPaymentDeadlines(),
                    Sellers = sellers.ToList(),
                    Buyers = buyers.ToList()
                };

                if (Request.Cookies.TryGetValue("LastSellerId", out var sellerIdStr) &&
                    int.TryParse(sellerIdStr, out var sellerId))
                {
                    var sellerDto = await _mediator.Send(new GetSellerByIdQuery(sellerId));
                    command.SellerDto = sellerDto;
                }

                return View(command);
            }
            catch (Exception)
            {
                this.SetNotification("error", "Wystąpił błąd podczas przygotowywania formularza faktury.");
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateFullInvoiceCommand model)
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

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditFullInvoiceCommand model)
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

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var dto = await _mediator.Send(new GetInvoiceByNumberQuery(id));
                return View(dto);
            }
            catch (Exception)
            {
                this.SetNotification("error", "Wystąpił błąd podczas pobierania faktury.");
                return RedirectToAction(nameof(Index));
            }
        }       
  
        public async Task<IActionResult> Edit(int id)
        {
            try
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
            catch (Exception)
            {
                this.SetNotification("error", "Wystąpił błąd podczas pobierania faktury.");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteInvoiceCommand { Id = id };
            this.SetNotification("success", $"Usunięto fakturę.");
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var invoices = await _mediator.Send(new GetAllInvoicesQuery());
                return View(invoices);
            }
            catch (Exception)
            {
                this.SetNotification("error", "Wystąpił błąd podczas ładowania listy faktur.");
                return View(new List<InvoiceDto>()); // lub RedirectToAction("Home", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBuyerByVatNumber(string nip)
        {
            try
            {
                var buyer = await _mediator.Send(new GetBuyerByNipQuery(nip));

                if (buyer == null)
                    return NotFound("Nie znaleziono kontrahenta");

                return Json(buyer);
            }
            catch (Exception)
            {
                return StatusCode(500, "Wystąpił błąd podczas wyszukiwania kontrahenta.");
            }
        }

        [HttpGet("Invoice/Download/{id}")]
        public async Task<IActionResult> DownloadPDF(int id)
        {
            try
            {
                var pdfBytes = await _mediator.Send(new GeneratePdfInvoiceCommand(id));

                if (pdfBytes == null || pdfBytes.Length == 0)
                    return NotFound("Nie udało się wygenerować pliku PDF.");

                var fileName = $"Faktura_{id}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                this.SetNotification("error", $"Wystąpił błąd podczas pobierania faktury PDF: {ex}");
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
