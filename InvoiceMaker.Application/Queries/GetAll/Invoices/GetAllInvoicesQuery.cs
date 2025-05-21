using InvoiceMaker.Application.Dto;
using MediatR;


namespace InvoiceMaker.Application.Queries.GetAll.Invoices
{
    public class GetAllInvoicesQuery : IRequest<IEnumerable<InvoiceDto>>
    {

    }
}
