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
    public class InvoiceController : ControllerBase
    {
        // GET: api/<InvoiceController>
        [HttpGet]
        public async Task<Invoice> SingleInvoice()
        {
            return new Invoice();
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{startDate}/{endDate}")]
        public async Task<List<Invoice>> InvoiceList(DateTime startDate, DateTime endDate)
        {
            return new List<Invoice>();
        }

        // POST api/<InvoiceController>
        [HttpPost]
        public async Task<Invoice> InvoiceSave([FromBody] InvoiceSaveResource request)
        {


            return new Invoice();
        }
    }
}
