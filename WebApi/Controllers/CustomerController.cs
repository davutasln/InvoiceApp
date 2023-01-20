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
    public class CustomerController : ControllerBase
    {
        private readonly CustomerManager _manager;

        public CustomerController(InvoiceAppDbContext context)
        {
            _manager = new CustomerManager(context);
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<Result<Customer>> Get()
        {
            try
            {
                return _manager.Get();
            }
            catch (Exception ex)
            {
                return new Result<Customer>()
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

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<Result<Customer>> Post([FromBody] Customer request)
        {
            var Return = new Result<Customer>() { Meta = new Meta() };
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Result<Customer>()
                    {
                        Meta = new Meta
                        {
                            IsSuccess = false,
                            Error = "model.is.not.valid",
                            ErrorMessage = "Request modeli hatalı. Request body kontrol ediniz."
                        }
                    };
                }

                return _manager.Post(request);
            }
            catch (Exception ex)
            {
                return new Result<Customer>()
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
