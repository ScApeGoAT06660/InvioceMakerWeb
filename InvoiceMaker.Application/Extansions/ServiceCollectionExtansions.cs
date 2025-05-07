using AutoMapper;
using InvoiceMaker.Application.Commands.Create;
using InvoiceMaker.Application.Commands.CreateBuyers;
using InvoiceMaker.Application.Commands.CreateItems;
using InvoiceMaker.Application.Commands.CreateSeller;
using InvoiceMaker.Application.Mappings;
using InvoiceMaker.Application.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IUserContext, UserContext>();
        }
    }
}
