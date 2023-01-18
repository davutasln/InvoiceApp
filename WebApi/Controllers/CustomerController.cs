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
        private readonly InvoiceAppDbContext _context;
        private readonly CustomerManager _manager;

        public CustomerController(InvoiceAppDbContext context)
        {
            this._context = context;
            this._manager = new CustomerManager(context);
        }


        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<Result<Customer>> Get()
        {
            try
            {
                return _manager.GetAll();
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
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "model.is.not.valid";
                    Return.Meta.ErrorMessage = "Request modeli hatalı. Request body kontrol ediniz.";
                    return Return;
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

        //// GET: api/<CustomerController>
        //[HttpGet]
        //public async Task<Result<Customer>> Get()
        //{
        //    var Return = new Result<Customer>() { Meta = new Meta() };
        //    try
        //    {
        //        Return.Entities = _context.Customers.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        Return.Meta.IsSuccess = false;
        //        Return.Meta.Error = "unexpected.error";
        //        Return.Meta.ErrorMessage = ex.Message;
        //        return Return;
        //    }

        //    Return.Meta.IsSuccess = true;
        //    return Return;
        //}

        //// POST api/<CustomerController>
        //[HttpPost]
        //public async Task<Result<Customer>> Post([FromBody] Customer request)
        //{
        //    var Return = new Result<Customer>() { Meta = new Meta() };
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            Return.Meta.IsSuccess = false;
        //            Return.Meta.Error = "model.is.not.valid";
        //            Return.Meta.ErrorMessage = "Request modeli hatalı. Request body kontrol ediniz.";
        //            return Return;
        //        }

        //        if (string.IsNullOrEmpty(request.TaxNumber))
        //        {
        //            Return.Meta.IsSuccess = false;
        //            Return.Meta.Error = "TaxNumber.is.empty";
        //            Return.Meta.ErrorMessage = "Vergi numarası zorunludur. Vergi numarasını lütfen doldurunuz.";
        //            return Return;
        //        }

        //        if (string.IsNullOrEmpty(request.Title))
        //        {
        //            Return.Meta.IsSuccess = false;
        //            Return.Meta.Error = "Title.is.empty";
        //            Return.Meta.ErrorMessage = "Title zorunludur. Title bilgisini lütfen doldurunuz.";
        //            return Return;
        //        }

        //        if (string.IsNullOrEmpty(request.Address))
        //        {
        //            Return.Meta.IsSuccess = false;
        //            Return.Meta.Error = "Address.is.empty";
        //            Return.Meta.ErrorMessage = "Address zorunludur. Address bilgisini lütfen doldurunuz.";
        //            return Return;
        //        }

        //        if (string.IsNullOrEmpty(request.Email))
        //        {
        //            Return.Meta.IsSuccess = false;
        //            Return.Meta.Error = "Email.is.empty";
        //            Return.Meta.ErrorMessage = "Email zorunludur. Email bilgisini lütfen doldurunuz.";
        //            return Return;
        //        }

        //        request.CreatedDate = DateTime.Now;
        //        _context.Customers.Add(request);
        //        _context.SaveChanges();

        //        Return.Entity = request;
        //    }
        //    catch (Exception ex)
        //    {
        //        Return.Meta.IsSuccess = false;
        //        Return.Meta.Error = "unexpected.error";
        //        Return.Meta.ErrorMessage = ex.Message;
        //        return Return;
        //    }

        //    Return.Meta.IsSuccess = true;
        //    return Return;
        //}
    }
}
