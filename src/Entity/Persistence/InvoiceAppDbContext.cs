using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Persistence
{
    public class InvoiceAppDbContext : DbContext
    {
        public InvoiceAppDbContext(DbContextOptions<InvoiceAppDbContext> options) : base(options)
        {
            DbInitializer.Initialize(this);
        }

        protected InvoiceAppDbContext() : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceLine> InvoiceLines { get; set; }
    }
}
