using InvoiceMaker.Application.Mappings;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using FluentValidation;
using InvoiceMaker.Application.Dto.Validators;
using InvoiceMaker.Application.Dto;
using InvoiceMaker.Application.Commands.Create;
using MediatR;
using InvoiceMaker.Application.Commands.CreateSeller;
using InvoiceMaker.Application.Commands.CreateBuyers;
using InvoiceMaker.Application.Commands.CreateItems;
using InvoiceMaker.Application.User;
using AutoMapper;

namespace InvoiceMaker.Application.Extansions
{
    public static class ServiceCollectionExtansions
    {
        public static void AddApplication(this IServiceCollection services)
        {

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new InvoiceMappingProfile(userContext));
            }).CreateMapper());

            //Commands
            services.AddMediatR(typeof(CreateFullInvoiceCommand));
            services.AddMediatR(typeof(CreateInvoiceCommand));
            services.AddMediatR(typeof(CreateSellerCommand));
            services.AddMediatR(typeof(CreateBuyerCommand));
            services.AddMediatR(typeof(CreateItemCommand));

            //Validators
            services.AddValidatorsFromAssemblyContaining<TraderDtoValidator>()
                    .AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<ItemDtoValidator>()
                    .AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<CreateInvoiceCommandValidator>()
                    .AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<SellerCommandValidator>()
                    .AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();

            services.AddScoped<IUserContext, UserContext>();
        }
    }
}
