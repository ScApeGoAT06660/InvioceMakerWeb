using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Domain.Entities
{
    public class Seller : Trader
    {
        public string? BankAccount { get; set; }
        public string? Bank { get; set; }
        public string? SWIFT { get; set; }
        public string? LogoPath { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedById { get; set; }
    }
}
