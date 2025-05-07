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
using AutoMapper;
using InvoiceMaker.Application.Commands.CreateSeller;
using InvoiceMaker.Application.Commands.DeleteSeller;

namespace InvoiceMaker.MVC.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IDropdownOptionsProvider _dropdownOptionsProvider;

        public SellerController(IMediator mediator, IMapper mapper, IDropdownOptionsProvider dropdownOptionsProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dropdownOptionsProvider = dropdownOptionsProvider;
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _mediator.Send(new GetSellerByIdQuery(id));
            EditSellerCommand edit = _mapper.Map<EditSellerCommand>(dto);

            return View(edit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditSellerCommand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SetNotification("success", $"Zedytowano sprzedawcę {model.Name}.");

            await _mediator.Send(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _mediator.Send(new GetSellerByIdQuery(id));
            return View(dto);
        }

        public IActionResult Create()
        {
            var command = new CreateSellerCommand();
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSellerCommand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SetNotification("success", $"Utworzono sprzedawcę {model.Name}.");

            await _mediator.Send(model);

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteSellerCommand { Id = id };
            this.SetNotification("success", $"Usunięto sprzedawcę.");
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var seller = await _mediator.Send(new GetAllSellersQuery());
            return View(seller);
        }
    }
}
