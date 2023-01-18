using Business.Concrete;
using DataAccess.Concrete;
using Entity.Models;
using Entity.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceAppDbContext _context;

        public InvoiceController(InvoiceAppDbContext context)
        {
            this._context = context;
        }


        // GET: api/<InvoiceController>
        [HttpGet]
        public async Task<Result<Invoice>> Get()
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

        // GET api/<InvoiceController>/bydate
        [HttpPost("bydate")]
        public async Task<Result<Invoice>> GetByDate([FromBody] ListRequestModel listRequestModel)
        {
            var Return = new Result<Invoice>() { Meta = new Meta() };
            try
            {
                if (!ModelState.IsValid)
                {
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "model.is.not.valid";
                    Return.Meta.ErrorMessage = "Request modeli hatalı. Request body kontrol ediniz.";
                    return Return;
                }

                var invoices = _context.Invoices.ToList();

                var startDate = DateTime.Parse(listRequestModel.startDate);
                var endDate = DateTime.Parse(listRequestModel.endDate);

                if (listRequestModel.startDate != null)
                {
                    invoices = invoices.Where(c => c.InvoiceDate > startDate).ToList();
                }

                if (listRequestModel.startDate != null)
                {
                    invoices = invoices.Where(c => c.InvoiceDate < endDate).ToList();
                }

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

        // POST api/<InvoiceController>
        [HttpPost]
        public async Task<Result<Invoice>> PostInvoice([FromBody] InvoiceSaveResource request)
        {
            var Return = new Result<Invoice>() { Meta = new Meta() };
            try
            {
                if (!ModelState.IsValid)
                {
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "model.is.not.valid";
                    Return.Meta.ErrorMessage = "Request modeli hatalı. Request body kontrol ediniz.";
                    return Return;
                }

                Customer customer = null;
                if (request.CustomerId > 0)
                {
                    customer = _context.Customers.FirstOrDefault(c => c.ID == request.CustomerId);
                }

                if (request.CustomerId == 0 || customer == null)
                {
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "customer.is.not.found";
                    Return.Meta.ErrorMessage = "Seçilen müşteri bulunamadı. Lütfen mevcut bir möüşteri seçin veya yeni bir müşteri ekleyin";
                    return Return;
                }

                if (request.InvoiceLines != null)
                {
                    var invoice = new Invoice
                    {
                        CustomerId = customer.ID,
                        InvoiceNumber = StringProcess.GenerateInvoiceNuumber(),
                        TotalAmount = request.InvoiceLines.Sum(s => s.Quantity * s.Price),
                        InvoiceDate = DateTime.Now,
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
