using FluentValidation.AspNetCore;
using InvoiceMaker.Application.Extansions;
using InvoiceMaker.Infrastructure.Extensions;
using InvoiceMaker.Infrastructure.Persistence;
using InvoiceMaker.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using InvoiceMaker.Application.Dto.Validators;
using FluentValidation;
using InvoiceMaker.MVC;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("pl-PL");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

var scope = app.Services.CreateScope();

var seed = scope.ServiceProvider.GetRequiredService<InvoiceMakerSeeder>();

await seed.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();
