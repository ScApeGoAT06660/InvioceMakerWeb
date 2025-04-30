using InvoiceMaker.Application.User;
using InvoiceMaker.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Queries.GetInvoiceNumber
{
    public class GetNewInvoiceNumberQueryHandler : IRequestHandler<GetNewInvoiceNumberQuery, string>
    {
        private readonly IInvoiceMakerRepository _repository;
        private readonly IUserContext _userContext;

        public GetNewInvoiceNumberQueryHandler(IInvoiceMakerRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task<string> Handle(GetNewInvoiceNumberQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentUser().Id;
            return await _repository.ReturnNewInvoiceNumber(userId);
        }
    }
}
