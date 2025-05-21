using AutoMapper;
using InvoiceMaker.Application.Commands.CreateBuyers;
using InvoiceMaker.Application.Commands.DeleteBuyer;
using InvoiceMaker.Application.Commands.EditBuyer;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.Queries.GetAll.Buyers;
using InvoiceMaker.Application.Queries.GetBy.BuyerId;
using InvoiceMaker.MVC.Extensions;
using InvoiceMaker.MVC.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var dto = await _mediator.Send(new GetBuyerByIdQuery(id));
                return View(dto);
            }
            catch (Exception)
            {
                this.SetNotification("error", "Nie udało się pobrać danych kontrahenta.");
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var dto = await _mediator.Send(new GetBuyerByIdQuery(id));
                EditBuyerCommand edit = _mapper.Map<EditBuyerCommand>(dto);

                return View(edit);
            }
            catch (Exception)
            {
                this.SetNotification("error", "Nie udało się wczytać kontrahenta do edycji.");
                return RedirectToAction(nameof(Index));
            }
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
            try
            {
                var buyer = await _mediator.Send(new GetAllBuyersQuery());
                return View(buyer);
            }
            catch (Exception)
            {
                this.SetNotification("error", "Nie udało się pobrać listy kontrahentów.");
                return View(new List<BuyerDto>());
            }
        }
    }
}
