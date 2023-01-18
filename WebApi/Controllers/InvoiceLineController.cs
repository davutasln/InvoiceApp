using DataAccess.Concrete;
using Entity.Models;
using Entity.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceLineController : ControllerBase
    {
        private readonly InvoiceAppDbContext _context;

        public InvoiceLineController(InvoiceAppDbContext context)
        {
            this._context = context;
        }

        // GET api/<InvoiceLineController>/5
        [HttpGet("{invoiceId}")]
        public async Task<Result<InvoiceLine>> Get(int invoiceId)
        {
            var Return = new Result<InvoiceLine>() { Meta = new Meta(), Entities = new List<InvoiceLine>() };
            try
            {
                if (invoiceId <= 0)
                {
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "invoiceId.is.not.valid";
                    Return.Meta.ErrorMessage = "Geçerli bir fatura seçiniz. Seçtiğiniz fatura bulunamadı.";
                    return Return;
                }

                var invoice = _context.Invoices.FirstOrDefault(c => c.ID == invoiceId);

                if (invoice == null)
                {
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "invoiceId.is.not.valid";
                    Return.Meta.ErrorMessage = "Geçerli bir fatura seçiniz. Seçtiğiniz fatura bulunamadı.";
                    return Return;
                }

                Return.Entities = _context.InvoiceLines.Where(c => c.InvoiceId == invoiceId).ToList();
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
