using AutoMapper;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Domain;
using InvoiceMaker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.Services
{
    internal class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceMakerRepository _invoiceMakerRepository;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceMakerRepository invoiceMakerRepository, IMapper mapper)
        {
            _invoiceMakerRepository = invoiceMakerRepository;
            _mapper = mapper;
        }

        //public async Task<Buyer> CreateBuyer(BuyerDto buyerDto)
        //{
        //    var buyer = _mapper.Map<Buyer>(buyerDto);
        //    await _invoiceMakerRepository.CreateBuyer(buyer);
        //    return buyer;
        //}

        //public async Task<Invoice> CreateInvoice(InvoiceDto invoiceDto)
        //{
        //    var invoice = _mapper.Map<Invoice>(invoiceDto);
        //    await _invoiceMakerRepository.CreateInvoice(invoice);
        //    return invoice;
        //}

        //public async Task CreateItem(ItemDto itemDto)
        //{
        //    var item = _mapper.Map<Item>(itemDto);
        //    await _invoiceMakerRepository.CreateItem(item);
        //}

        //public async Task<Seller> CreateSeller(SellerDto sellerDto)
        //{
        //    var seller = _mapper.Map<Seller>(sellerDto);
        //    await _invoiceMakerRepository.CreateSeller(seller);
        //    return seller; 
        //}

        //public async Task<IEnumerable<BuyerDto>> GetAllBuyers()
        //{
        //    var buyer = await _invoiceMakerRepository.GetAllBuyers();
        //    var dtos = _mapper.Map<IEnumerable<BuyerDto>>(buyer);
        //    return dtos;
        //}

        //public async Task<IEnumerable<InvoiceDto>> GetAllInvoices()
        //{
        //    var invoices = await _invoiceMakerRepository.GetAllInvoices();
        //    var dtos = _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        //    return dtos;
        //}

        //public async Task<IEnumerable<SellerDto>> GetAllSellers()
        //{
        //    var seller = await _invoiceMakerRepository.GetAllSellers();
        //    var dtos = _mapper.Map<IEnumerable<SellerDto>>(seller);
        //    return dtos;
        //}
    }
}
