using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Dto
{
    public class SellerDto : TraderDto
    {
        public string? BankAccount { get; set; }
        public string? Bank { get; set; }
        public string? SWIFT { get; set; }
        public string? LogoPath { get; set; }
    }
}
