using InvoiceMaker.Domain;
using InvoiceMaker.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Infrastructure.Seeders
{
    public class InvoiceMakerSeeder
    {
        private readonly InvoiceMakerDbContext _dbContext;

        public InvoiceMakerSeeder(InvoiceMakerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                // Czy są już dane? Jeśli nie, dodaj
                if (!_dbContext.Sellers.Any() && !_dbContext.Buyers.Any() && !_dbContext.Invoices.Any())
                {
                    var seller = new Seller
                    {
                        Name = "Firma Testowa Sp. z o.o.",
                        VATID = "PL1234567890",
                        StreetAndNo = "ul. Testowa 12",
                        Postcode = "00-001",
                        City = "Warszawa",
                        BankAccount = "12345678901234567890123456",
                        Bank = "Bank Testowy",
                        SWIFT = "TESTPLPW",
                        LogoPath = null
                    };

                    var buyer = new Buyer
                    {
                        Name = "Klient Przykładowy",
                        VATID = "PL9876543210",
                        StreetAndNo = "ul. Klienta 5",
                        Postcode = "00-002",
                        City = "Kraków"
                    };

                    var invoice = new Invoice
                    {
                        Number = "FV/2025/01/01",
                        IssueDate = DateTime.Now,
                        SaleDate = DateTime.Now,
                        Place = "Warszawa",
                        PaymentType = "Przelew",
                        PaymentDeadline = "7 dni",
                        Notes = "Testowa faktura",
                        Seller = seller,
                        Buyer = buyer,
                        Items = new List<Item>
                {
                    new Item
                    {
                        Position = "1",
                        Name = "Usługa testowa",
                        Quantity = 1,
                        Unit = "szt.",
                        VAT = "23%",
                        Netto = 100m,
                        Brutto = 123m
                    }
                }
                    };

                    _dbContext.Invoices.Add(invoice);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

    }
}
