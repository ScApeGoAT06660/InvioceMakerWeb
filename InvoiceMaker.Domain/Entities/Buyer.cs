using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Domain.Entities
{
    public class Buyer : Trader
    {
        public bool isDeleted { get; set; }
        public string CreatedById { get; set; }
    }
}
