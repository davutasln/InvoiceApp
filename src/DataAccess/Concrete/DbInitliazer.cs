using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
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
                        Address = "Çukurova Üniversitesi Teknokent Sitesi Sarıçam/Adana",
                        Email = "davut@test.com",
                        Title = "Davut",
                        TaxNumber = GenerateTaxNumber(),
                        CreatedDate = DateTime.Now,
                    });

                _context.Customers.Add(
                    new Customer
                    {
                        Address = "542 W. Honey Creek St. Hattiesburg, MS 39401",
                        Email = "joel@test.com",
                        Title = "Joel",
                        TaxNumber = GenerateTaxNumber(),
                        CreatedDate = DateTime.Now,
                    });

                _context.Customers.Add(
                    new Customer
                    {
                        Address = "12 Winchester Road Avon, IN 46123",
                        Email = "ellie@test.com",
                        Title = "Ellie",
                        TaxNumber = GenerateTaxNumber(),
                        CreatedDate = DateTime.Now,
                    });

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Create Exception Error : {ex.Message}");
            }
        }

        public static string GenerateTaxNumber()
        {
            Random random = new Random();
            return $"{random.Next(1000000000, int.MaxValue)}";
        }
    }
}
