using AutoMapper;
using InvoiceMaker.Application.Commands.Create;
using InvoiceMaker.Application.Commands.CreateBuyers;
using InvoiceMaker.Application.Commands.CreateSeller;
using InvoiceMaker.Application.Commands.DeleteBuyer;
using InvoiceMaker.Application.Commands.DeleteInvoice;
using InvoiceMaker.Application.Commands.DeleteSeller;
using InvoiceMaker.Application.Commands.EditBuyer;
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

            this.SetNotification("success", $"Stoworzono fakture {model.InvoiceDto.Number}");

            await _mediator.Send(new CreateFullInvoiceCommand
            {
                SellerDto = model.SellerDto,
                BuyerDto = model.BuyerDto,
                InvoiceDto = model.InvoiceDto,
                ItemsDto = model.ItemsDto
            });

            return RedirectToAction(nameof(InvoiceIndex));
        }

        [HttpPost]
        [Route("Invoice/Edit/{id}")]
        public async Task<IActionResult> InvoiceEdit(int id, EditInvoiceCommand model)
        {

            model.PaymentType = model.SelectedPaymentOption;
            model.PaymentDeadline = model.SelectedPaymentDeadline;

            if (!ModelState.IsValid)
            {
                model.PaymentOptionsList = new SelectList(
                    _dropdownOptionsProvider.GetPaymentMethods(), "Value", "Text", model.SelectedPaymentOption
                );

                model.DeadlineOptionsList = new SelectList(
                    _dropdownOptionsProvider.GetPaymentDeadlines(), "Value", "Text", model.SelectedPaymentDeadline
                );

                if (model.Items == null || !model.Items.Any())
                {
                    model.Items = new List<ItemDto> { new ItemDto() };
                }

                return View(model);

            }

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
            var dto = await _mediator.Send(new GetInvoiceByNumberQuery(id));
            EditInvoiceCommand edit = _mapper.Map<EditInvoiceCommand>(dto);

            edit.SelectedPaymentOption = edit.PaymentType;
            edit.SelectedPaymentDeadline = edit.PaymentDeadline;

            edit.PaymentOptionsList = _dropdownOptionsProvider.GetPaymentMethods();

            edit.DeadlineOptionsList = _dropdownOptionsProvider.GetPaymentDeadlines();

            if (edit.Items == null || !edit.Items.Any())
            {
                edit.Items = new List<ItemDto> { new ItemDto() };
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
            await _mediator.Send(command);
            return RedirectToAction(nameof(SellerIndex));
        }

        [HttpPost]
        [Route("Invoice/Delete/{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var command = new DeleteInvoiceCommand { Id = id };
            await _mediator.Send(command);
            return RedirectToAction(nameof(InvoiceIndex));
        }

        [HttpPost]
        [Route("Buyer/Delete/{id}")]
        public async Task<IActionResult> DeleteBuyer(int id)
        {
            var command = new DeleteBuyerCommand { Id = id };
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
