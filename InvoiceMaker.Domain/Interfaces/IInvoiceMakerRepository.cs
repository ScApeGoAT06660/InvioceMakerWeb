﻿using InvoiceMaker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Domain.Interfaces
{
    public interface IInvoiceMakerRepository
    {
        Task CreateInvoice(Invoice invoice);    

        Task CreateSeller(Seller seller);

        Task CreateBuyer(Buyer buyer);

        Task CreateItem(Item item);
        Task<Trader?> GetByVATID(string value);
        Task<Seller?> GetSellerByVATID (string value);
        Task<Buyer?> GetBuyerByVATID(string value);
        Task<Invoice?> GetInvoiceByNumber(string value);
        Task<Invoice?> GetInvoiceById(int value);
        Task<IEnumerable<Invoice>> GetAllInvoices(string userId);
        Task<IEnumerable<Seller>> GetAllSellers(string userId);
        Task<Seller?> GetSellerByID(int value);
        Task<IEnumerable<Buyer>> GetAllBuyers(string userId);
        Task<Buyer?> GetBuyerByID(int value);
        Task DeleteSeller(int value);
        Task DeleteInvoice(int value);
        Task DeleteBuyer(int value);
        Task Commit();
        Task<string> ReturnNewInvoiceNumber(string userId);
        Task<List<Item>> GetItemsByInvoiceId(int value);
    }
}
