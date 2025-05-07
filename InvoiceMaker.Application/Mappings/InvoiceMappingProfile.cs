using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using InvoiceMaker.Application.Commands.Create;
using InvoiceMaker.Application.Commands.CreateBuyers;
using InvoiceMaker.Application.Commands.CreateItems;
using InvoiceMaker.Application.Commands.CreateSeller;
using InvoiceMaker.Application.Commands.EditBuyer;
using InvoiceMaker.Application.Commands.EditInvoice;
using InvoiceMaker.Application.Commands.EditSeller;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.User;
using InvoiceMaker.Domain.Entities;

namespace InvoiceMaker.Application.Mappings
{
    internal class InvoiceMappingProfile : Profile
    {
        public InvoiceMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();

            CreateMap<InvoiceDto, Invoice>()
            .ForMember(dest => dest.Seller, opt => opt.Ignore())
            .ForMember(dest => dest.Buyer, opt => opt.Ignore());

            CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.IsEditable, opt => opt.MapFrom(src => user != null && src.CreatedById == user.Id));

            CreateMap<Seller, SellerDto>().ReverseMap()
            .IncludeMembers(src => src);

            CreateMap<Buyer, BuyerDto>().ReverseMap()
            .IncludeMembers(src => src);

            CreateMap<Item, ItemDto>().ReverseMap();
            
            CreateMap<EditSellerCommand, Seller>();

            CreateMap<SellerDto, EditSellerCommand>();
            CreateMap<BuyerDto, EditBuyerCommand>();            
            CreateMap<InvoiceDto, EditInvoiceCommand>();

            CreateMap<ItemDto, CreateItemCommand>();
            CreateMap<SellerDto, CreateSellerCommand>();
            CreateMap<BuyerDto, CreateBuyerCommand>();
            CreateMap<InvoiceDto, CreateInvoiceCommand>();
        }
    }
}


