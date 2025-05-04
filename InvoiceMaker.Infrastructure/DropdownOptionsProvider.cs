using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceMaker.MVC.Services
{
    public class DropdownOptionsProvider : IDropdownOptionsProvider
    {
        public List<SelectListItem> GetPaymentMethods() => new()
        {
            new SelectListItem { Value = "gotówka", Text = "gotówka" },
            new SelectListItem { Value = "karta kredytowa", Text = "karta kredytowa" },
            new SelectListItem { Value = "przelew", Text = "przelew" }
        };

            public List<SelectListItem> GetPaymentDeadlines() => new()
        {
            new SelectListItem { Value = "3 dni", Text = "3 dni" },
            new SelectListItem { Value = "7 dni", Text = "7 dni" },
            new SelectListItem { Value = "14 dni", Text = "14 dni" },
            new SelectListItem { Value = "30 dni", Text = "30 dni" }
        };
    }
}
