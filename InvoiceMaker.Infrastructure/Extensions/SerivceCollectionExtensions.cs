﻿using InvoiceMaker.Application.Services;
using InvoiceMaker.Domain.Interfaces;
using InvoiceMaker.Infrastructure.Persistence;
using InvoiceMaker.Infrastructure.Repositories;
using InvoiceMaker.Infrastructure.Seeders;
using InvoiceMaker.Infrastructure.Services;
using InvoiceMaker.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Infrastructure.Extensions
{
    public static class SerivceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InvoiceMakerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<InvoiceMakerDbContext>();


            services.AddScoped<InvoiceMakerSeeder>();
            services.AddScoped<IInvoiceMakerRepository, InvoiceMakerRepository>();
            services.AddScoped<IMRiFService, MRiFService>();
            services.AddScoped<IPDFSharpService, PDFSharpService>();
            services.AddScoped<IDropdownOptionsProvider, DropdownOptionsProvider>();
        }
    }
}
