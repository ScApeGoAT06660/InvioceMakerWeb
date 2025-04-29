using InvoiceMaker.Domain;
using InvoiceMaker.Domain.Interfaces;
using InvoiceMaker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Infrastructure.Repositories
{
    internal class InvoiceMakerRepository : IInvoiceMakerRepository
    {
        private readonly InvoiceMakerDbContext _dbContext;

        public InvoiceMakerRepository(InvoiceMakerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateBuyer(Buyer buyer)
        {
            _dbContext.Add(buyer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateInvoice(Invoice invoice)
        {
            _dbContext.Add(invoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateSeller(Seller seller)
        {
            _dbContext.Add(seller);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateItem(Item item)
        {
            _dbContext.Add(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Trader?> GetByVATID(string value)
        {     
            var trader =  await _dbContext.Traders.FirstOrDefaultAsync(x=> x.VATID.ToLower() == value.ToLower());  
            return trader;
        }

        public async Task<Seller?> GetSellerByVATID(string value)
        {
            var trader = await _dbContext.Traders
               .FirstOrDefaultAsync(x => x.VATID.ToLower() == value.ToLower());

            if (trader == null)
                return null;

            var seller = await _dbContext.Sellers
                .FirstOrDefaultAsync(x => x.Id == trader.Id);

            return seller;
        }

        public async Task<Buyer?> GetBuyerByVATID(string value)
        {
            var trader = await _dbContext.Traders
               .FirstOrDefaultAsync(x => x.VATID.ToLower() == value.ToLower());

            if (trader == null)
                return null;

            var buyer = await _dbContext.Buyers
                .FirstOrDefaultAsync(x => x.Id == trader.Id);

            return buyer;
        }

        public async Task<Invoice?> GetInvoiceByNumber(string value)
        {
            var invoice = await _dbContext.Invoices
            .FirstOrDefaultAsync(x => x.Number == value);

            return invoice;
        }

        public async Task<Invoice?> GetInvoiceById(int value)
        {
            int id = Convert.ToInt32(value);

            var invoice = await _dbContext.Invoices
            .Include(x => x.Items)
            .Include(x => x.Seller)
            .Include(x => x.Buyer)
            .FirstOrDefaultAsync(x => x.Id == id);

            return invoice;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoices(string userId)
        {
            var invoices = await _dbContext.Invoices.Where(x => x.CreatedById == userId)
                .ToListAsync();
            return invoices;
        }

        public async Task<IEnumerable<Seller>> GetAllSellers()
        {
            var sellers = await _dbContext.Sellers.Where(x => x.IsDeleted == false).ToListAsync();
            return sellers;
        }

        public async Task<IEnumerable<Buyer>> GetAllBuyers()
        {
            var buyer = await _dbContext.Buyers.Where(x => x.isDeleted == false).ToListAsync();
            return buyer;
        }

        public Task Commit() => _dbContext.SaveChangesAsync();

        public async Task<Seller?> GetSellerByID(int value)
        {
            var seller = await _dbContext.Sellers.Where(x => x.Id == value).FirstOrDefaultAsync();
            return seller;
        }

        public async Task DeleteSeller(int value)
        {
            var seller = await _dbContext.Sellers.Where(x => x.Id == value).FirstOrDefaultAsync();

            bool exist = await _dbContext.Invoices.AnyAsync(i =>  i.SellerId == seller.Id);

            if(exist)
            {
                seller.IsDeleted = true;
            }
            else
            {
                _dbContext.Sellers.Remove(seller);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteInvoice(int value)
        {
            var invoice = await _dbContext.Invoices
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == value);

            _dbContext.Invoices.Remove(invoice);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Buyer?> GetBuyerByID(int value)
        {
            var buyer = await _dbContext.Buyers.Where(x => x.Id == value).FirstOrDefaultAsync();
            return buyer;
        }

        public async Task DeleteBuyer(int value)
        {
            var buyer = await _dbContext.Buyers.Where(x => x.Id == value).FirstOrDefaultAsync();

            bool exist = await _dbContext.Invoices.AnyAsync(i => i.BuyerId == buyer.Id);

            if (exist)
            {
                buyer.isDeleted = true;
            }
            else
            {
                _dbContext.Buyers.Remove(buyer);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
