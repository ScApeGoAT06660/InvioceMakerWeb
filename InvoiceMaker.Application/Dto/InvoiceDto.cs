﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Dto
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Numer faktury jest wymagany")]
        [StringLength(50, ErrorMessage = "Numer faktury nie może przekraczać 50 znaków")]
        [Display(Name = "Numer")]
        public string? Number { get; set; } = default!;

        [Required(ErrorMessage = "Data wystawienia jest wymagana")]
        [DataType(DataType.Date)]
        [Display(Name = "Data wystawienia")]
        public DateTime IssueDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Data sprzedaży jest wymagana")]
        [DataType(DataType.Date)]
        [Display(Name = "Data sprzedaży")]
        public DateTime SaleDate { get; set; } = DateTime.Today;
        [Display(Name = "Miejsce")]
        public string? Place { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public string? BuyerType { get; set; }
        public List<ItemDto>? Items { get; set; }
        [Display(Name = "Płatność")]
        public string? PaymentType { get; set; }
        [Display(Name = "Termin płatności")]
        public string? PaymentDeadline { get; set; }
        [Display(Name = "Podpis sprzedawcy")]
        public string? SellerSignature { get; set; }
        [Display(Name = "Podpis kupującego")]
        public string? BuyerSignature { get; set; }
        [Display(Name = "Uwagi")]
        public string? Notes { get; set; }

        public SellerDto? Seller { get; set; }
        public BuyerDto? Buyer { get; set; }
        public bool IsEditable { get; set; }
    }
}
