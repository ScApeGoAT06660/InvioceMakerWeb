using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Dto
{
    public class SellerDto : TraderDto
    {
        [Display(Name = "Numer konta")]
        public string? BankAccount { get; set; }
        [Display(Name = "Bank")]
        public string? Bank { get; set; }
        [Display(Name = "Swift")]
        public string? SWIFT { get; set; }
        [Display(Name = "Załaduj logo")]
        public string? LogoPath { get; set; }
    }
}
