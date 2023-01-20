using Business.Extensions;
using DataAccess.Concrete;
using Entity.Models;
using Entity.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class InvoiceManager
    {
        private readonly InvoiceAppDbContext _context;

        public InvoiceManager(InvoiceAppDbContext context)
        {
            this._context = context;
        }

        public Result<Invoice> Get()
        {
            var Return = new Result<Invoice>() { Meta = new Meta() };
            try
            {
                Return.Entities = _context.Invoices.Include(c => c.Customer).ToList();
            }
            catch (Exception ex)
            {
                Return.Meta.IsSuccess = false;
                Return.Meta.Error = "unexpected.error";
                Return.Meta.ErrorMessage = ex.Message;
                return Return;
            }

            Return.Meta.IsSuccess = true;
            return Return;
        }

        public Result<Invoice> GetByDate(ListRequestModel listRequestModel)
        {
            var Return = new Result<Invoice>() { Meta = new Meta() };
            try
            {
                var invoices = _context.Invoices.Include(c => c.Customer).ToList();

                if (listRequestModel.startDate != null)
                    invoices = invoices.Where(c => c.InvoiceDate > listRequestModel.startDate).ToList();

                if (listRequestModel.startDate != null)
                    invoices = invoices.Where(c => c.InvoiceDate < listRequestModel.endDate).ToList();

                Return.Entities = invoices;
            }
            catch (Exception ex)
            {
                Return.Meta.IsSuccess = false;
                Return.Meta.Error = "unexpected.error";
                Return.Meta.ErrorMessage = ex.Message;
                return Return;
            }

            Return.Meta.IsSuccess = true;
            return Return;
        }

        public Result<Invoice> PostInvoice(InvoiceSaveResource request)
        {
            var Return = new Result<Invoice>() { Meta = new Meta() };
            try
            {
                Customer customer = null;
                if (request.CustomerId > 0)
                {
                    customer = _context.Customers.FirstOrDefault(c => c.ID == request.CustomerId);
                }

                if (request.CustomerId == 0 || customer == null)
                {
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "customer.is.not.found";
                    Return.Meta.ErrorMessage = "Seçilen müşteri bulunamadı. Lütfen mevcut bir müşteri seçin veya yeni bir müşteri ekleyin";
                    return Return;
                }

                if (request.InvoiceLines != null)
                {
                    var invoice = new Invoice
                    {
                        CustomerId = customer.ID,
                        InvoiceNumber = StringProcess.GenerateInvoiceNuumber(),
                        TotalAmount = request.InvoiceLines.Sum(s => s.Quantity * s.Price),
                        InvoiceDate = DateProcess.GenerateRandomDate(),
                        CreatedDate = DateTime.Now,
                    };

                    _context.Invoices.Add(invoice);
                    _context.SaveChanges();

                    foreach (var lineItem in request.InvoiceLines)
                    {
                        var invoiceLine = new InvoiceLine
                        {
                            InvoiceId = invoice.ID,
                            ItemName = lineItem.ItemName,
                            Price = lineItem.Price,
                            Quantity = lineItem.Quantity,
                            CreatedDate = DateTime.Now
                        };

                        _context.InvoiceLines.Add(invoiceLine);
                    }

                    _context.SaveChanges();
                    Return.Entity = invoice;
                }
                else
                {
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "invoiceLine.is.not.found";
                    Return.Meta.ErrorMessage = "Fatura için ürün bilgisi giriniz.";
                    return Return;
                }

            }
            catch (Exception ex)
            {
                Return.Meta.IsSuccess = false;
                Return.Meta.Error = "unexpected.error";
                Return.Meta.ErrorMessage = ex.Message;
                return Return;
            }

            Return.Meta.IsSuccess = true;
            return Return;
        }
    }
}
