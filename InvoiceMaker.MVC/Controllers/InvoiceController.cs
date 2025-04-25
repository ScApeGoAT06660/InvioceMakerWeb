using Microsoft.AspNetCore.Mvc;
using InvoiceMaker.MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using InvoiceMaker.Application.Dto;
using MediatR;
using InvoiceMaker.Application.Queries;
using InvoiceMaker.Application.Commands.CreateFullInvoice;
using InvoiceMaker.Application.Commands.Create;
using InvoiceMaker.Application.Queries.GetAll;
using InvoiceMaker.Application.Queries.GetBy;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using InvoiceMaker.MVC.Extensions;
using InvoiceMaker.Application.Commands.EditInvoice;
using InvoiceMaker.Application.Commands.EditSeller;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InvoiceMaker.MVC.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public InvoiceController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IActionResult CreateFullInvoice()
        {

            var command = new CreateFullInvoiceCommand
            {
                ItemsDto = new List<ItemDto> { new ItemDto() },
                PaymentOptionsList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "gotówka" },
                    new SelectListItem { Value = "2", Text = "karta kredytowa" },
                    new SelectListItem { Value = "3", Text = "przelew" }
                },

                DeadlineOptionsList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "3 dni" },
                    new SelectListItem { Value = "2", Text = "7 dni" },
                    new SelectListItem { Value = "3", Text = "14 dni" },
                    new SelectListItem { Value = "3", Text = "30 dni" }
                }
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
                model.PaymentOptionsList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "gotówka", Text = "gotówka" },
                    new SelectListItem { Value = "karta kredytowa", Text = "karta kredytowa" },
                    new SelectListItem { Value = "przelew", Text = "przelew" }
                };

                model.DeadlineOptionsList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "3 dni", Text = "3 dni" },
                    new SelectListItem { Value = "7 dni", Text = "7 dni" },
                    new SelectListItem { Value = "14 dni", Text = "14 dni" },
                    new SelectListItem { Value = "30 dni", Text = "30 dni" }
                };

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
                new List<string> { "gotówka", "karta kredytowa", "przelew" },
                selectedValue: model.SelectedPaymentOption
                );

                model.DeadlineOptionsList = new SelectList(
                    new List<string> { "3 dni", "7 dni", "14 dni", "30 dni" },
                    selectedValue: model.SelectedPaymentDeadline
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

        [Route("Invoice/Edit/{id}")]
        public async Task<IActionResult> InvoiceEdit(int id)
        {
            var dto = await _mediator.Send(new GetInvoiceByNumberQuery(id));
            EditInvoiceCommand edit = _mapper.Map<EditInvoiceCommand>(dto);

            edit.SelectedPaymentOption = edit.PaymentType;
            edit.SelectedPaymentDeadline = edit.PaymentDeadline;

            edit.PaymentOptionsList = new List<SelectListItem>
            {
                new SelectListItem { Value = "gotówka", Text = "gotówka" },
                new SelectListItem { Value = "karta kredytowa", Text = "karta kredytowa" },
                new SelectListItem { Value = "przelew", Text = "przelew" }
            };

            edit.DeadlineOptionsList = new List<SelectListItem>
            {
                new SelectListItem { Value = "3 dni", Text = "3 dni" },
                new SelectListItem { Value = "7 dni", Text = "7 dni" },
                new SelectListItem { Value = "14 dni", Text = "14 dni" },
                new SelectListItem { Value = "30 dni", Text = "30 dni" }
            };

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

        [Route("Seller/Details/{id}")]
        public async Task<IActionResult> SellerDetails(int id)
        {
            var dto = await _mediator.Send(new GetSellerByIdQuery(id));
            return View(dto);
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
    }
}
