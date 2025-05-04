using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceMaker.MVC.Services
{
    public interface IDropdownOptionsProvider
    {
        List<SelectListItem> GetPaymentDeadlines();
        List<SelectListItem> GetPaymentMethods();
    }
}