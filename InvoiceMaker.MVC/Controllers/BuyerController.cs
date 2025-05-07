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
    public class BuyerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IDropdownOptionsProvider _dropdownOptionsProvider;

        public BuyerController(IMediator mediator, IMapper mapper, IDropdownOptionsProvider dropdownOptionsProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dropdownOptionsProvider = dropdownOptionsProvider;
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _mediator.Send(new GetBuyerByIdQuery(id));
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _mediator.Send(new GetBuyerByIdQuery(id));
            EditBuyerCommand edit = _mapper.Map<EditBuyerCommand>(dto);

            return View(edit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBuyerCommand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SetNotification("success", $"Zedytowano kontrahenta {model.Name}.");

            await _mediator.Send(model);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            var command = new CreateBuyerCommand();
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBuyerCommand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SetNotification("success", $"Utworzono kontrahenta {model.Name}.");

            await _mediator.Send(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteBuyerCommand { Id = id };
            this.SetNotification("success", $"Usunięto kontrahenta.");
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var buyer = await _mediator.Send(new GetAllBuyersQuery());
            return View(buyer);
        }
    }
}
