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
        private readonly InvoiceManager _manager;

        public InvoiceController(InvoiceAppDbContext context)
        {
            _manager = new InvoiceManager(context);
        }

        // GET: api/<InvoiceController>
        [HttpGet]
        public async Task<Result<Invoice>> Get()
        {
            try
            {
                return _manager.Get();
            }
            catch (Exception ex)
            {
                return new Result<Invoice>()
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

        // GET api/<InvoiceController>/bydate
        [HttpPost("bydate")]
        public async Task<Result<Invoice>> GetByDate([FromBody] ListRequestModel listRequestModel)
        {
            var Return = new Result<Invoice>() { Meta = new Meta() };
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Result<Invoice>()
                    {
                        Meta = new Meta
                        {
                            IsSuccess = false,
                            Error = "model.is.not.valid",
                            ErrorMessage = "Request modeli hatalı. Request body kontrol ediniz."
                        }
                    };
                }

                return _manager.GetByDate(listRequestModel);
            }
            catch (Exception ex)
            {
                return new Result<Invoice>()
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

        // POST api/<InvoiceController>
        [HttpPost]
        public async Task<Result<Invoice>> PostInvoice([FromBody] InvoiceSaveResource request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Result<Invoice>()
                    {
                        Meta = new Meta
                        {
                            IsSuccess = false,
                            Error = "model.is.not.valid",
                            ErrorMessage = "Request modeli hatalı. Request body kontrol ediniz."
                        }
                    };
                }

                return _manager.PostInvoice(request);
            }
            catch (Exception ex)
            {
                return new Result<Invoice>()
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
