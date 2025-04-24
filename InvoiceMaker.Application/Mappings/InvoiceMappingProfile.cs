using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using InvoiceMaker.Application.Commands;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.User;
using InvoiceMaker.Domain;

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

            CreateMap<InvoiceDto, EditInvoiceCommand>();
        }
    }
}
