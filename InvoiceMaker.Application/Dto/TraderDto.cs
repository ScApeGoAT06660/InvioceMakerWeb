using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Dto
{
    public class TraderDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string VATID { get; set; } = default!;
        public string? StreetAndNo { get; set; }
        public string? Postcode { get; set; }
        public string? City { get; set; }
    }
}
