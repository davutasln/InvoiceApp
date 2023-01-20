using Business.Concrete;
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
        private readonly InvoiceLineManager _manager;

        public InvoiceLineController(InvoiceAppDbContext context)
        {
            _manager = new InvoiceLineManager(context);
        }

        // GET api/<InvoiceLineController>/5
        [HttpGet("{invoiceId}")]
        public async Task<Result<InvoiceLine>> Get(int invoiceId)
        {
            try
            {
                return _manager.Get(invoiceId);
            }
            catch (Exception ex)
            {
                return new Result<InvoiceLine>()
                {
                    Meta = new Meta
                    {
                        IsSuccess = false,
                        Error = "unexpected.error",
                        ErrorMessage = ex.Message,
                    }
                };
            }
        }
    }
}
