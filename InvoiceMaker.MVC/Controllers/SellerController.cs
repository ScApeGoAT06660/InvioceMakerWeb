using AutoMapper;
using InvoiceMaker.Application.Commands.CreateSeller;
using InvoiceMaker.Application.Commands.DeleteSeller;
using InvoiceMaker.Application.Commands.EditSeller;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.Queries.GetAll.Sellers;
using InvoiceMaker.Application.Queries.GetBy.SellerId;
using InvoiceMaker.MVC.Extensions;
using InvoiceMaker.MVC.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var dto = await _mediator.Send(new GetSellerByIdQuery(id));
                var edit = _mapper.Map<EditSellerCommand>(dto);
                return View(edit);
            }
            catch (Exception)
            {
                this.SetNotification("error", "Nie udało się wczytać danych sprzedawcy.");
                return RedirectToAction(nameof(Index));
            }
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
            try
            {
                var dto = await _mediator.Send(new GetSellerByIdQuery(id));
                return View(dto);
            }
            catch (Exception)
            {
                this.SetNotification("error", "Nie udało się pobrać szczegółów sprzedawcy.");
                return RedirectToAction(nameof(Index));
            }
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
            try
            {
                var seller = await _mediator.Send(new GetAllSellersQuery());
                return View(seller);
            }
            catch (Exception)
            {
                this.SetNotification("error", "Nie udało się pobrać listy sprzedawców.");
                return View(new List<SellerDto>());
            }
        }
    }
}
