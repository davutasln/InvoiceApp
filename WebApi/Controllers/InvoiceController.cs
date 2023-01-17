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
        public IEnumerable<Invoice> Get()
        {
            return new List<Invoice>();
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{startDate}/{endDate}")]
        public async Task<Invoice> Get(DateTime startDate, DateTime endDate)
        {
            return new Invoice();
        }

        // POST api/<InvoiceController>
        [HttpPost]
        public async Task<Invoice> PostInvoice([FromBody] InvoiceSaveResource request)
        {


            return new Invoice();
        }
    }
}
