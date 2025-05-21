using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMaker.Domain.Interfaces;
using MediatR;
using AutoMapper;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain.Entities;

namespace InvoiceMaker.Application.Commands.CreateItems
{
    internal class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;

        public CreateItemCommandHandler(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var item = _mapper.Map<Item>(request);
                await _invoiceMakerRepository.CreateItem(item);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Wystąpił błąd podczas tworzenia pozycji faktury.", ex);
            }
        }
    }
}
