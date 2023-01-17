using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(InvoiceAppDbContext _context)
        {
            try
            {
                _context.Database.EnsureCreated();

                if (_context.Customers.Any())
                    return; // DB has been seeded

                _context.Customers.Add(
                    new Customer
                    {
                        Address = "Çukurova Üniversitesi Teknokent Sitesi B Blok B110 Sarıçam/Adana",
                        Email = "davut@test.com",
                        Title = "Davut ASLAN",
                        TaxNumber = "11234325",
                        CreatedDate = DateTime.Now,
                    });

                _context.Customers.Add(
                    new Customer
                    {
                        Address = "Çukurova Üniversitesi Teknokent Sitesi B Blok B110 Sarıçam/Adana",
                        Email = "joel@test.com",
                        Title = "Joel",
                        TaxNumber = "11234325",
                        CreatedDate = DateTime.Now,
                    });

                _context.Customers.Add(
                    new Customer
                    {
                        Address = "Çukurova Üniversitesi Bilgisayar Mühendisliği Sarıçam/Adana",
                        Email = "ellie@test.com",
                        Title = "Ellie",
                        TaxNumber = "11234325",
                        CreatedDate = DateTime.Now,
                    });

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Create Exception Error : {ex.Message}");
            }
        }
    }
}
