using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using InvoiceMaker.Domain.Entities;

namespace InvoiceMaker.Infrastructure.Persistence
{
    public class InvoiceMakerDbContext : IdentityDbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Trader> Traders { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Buyer> Buyers { get; set; }

        public InvoiceMakerDbContext(DbContextOptions<InvoiceMakerDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trader>().ToTable("Traders");
            modelBuilder.Entity<Seller>().ToTable("Sellers");
            modelBuilder.Entity<Buyer>().ToTable("Buyers");

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Seller)
                .WithMany()
                .HasForeignKey(i => i.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Buyer)
                .WithMany()
                .HasForeignKey(i => i.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);


        }

        
    }
}
