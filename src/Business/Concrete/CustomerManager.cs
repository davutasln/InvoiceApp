using DataAccess.Concrete;
using Entity.Models;
using Entity.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager
    {
        private readonly InvoiceAppDbContext _context;

        public CustomerManager(InvoiceAppDbContext context)
        {
            this._context = context;
        }

        public  Result<Customer> Get()
        {
            var Return = new Result<Customer>() { Meta = new Meta() };
            try
            {
                Return.Entities = _context.Customers.ToList();
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

        public Result<Customer> Post(Customer request)
        {
            var Return = new Result<Customer>() { Meta = new Meta() };
            try
            {
                if (string.IsNullOrEmpty(request.TaxNumber))
                {
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "TaxNumber.is.empty";
                    Return.Meta.ErrorMessage = "Vergi numarası zorunludur. Vergi numarasını lütfen doldurunuz.";
                    return Return;
                }

                if (string.IsNullOrEmpty(request.Title))
                {
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "Title.is.empty";
                    Return.Meta.ErrorMessage = "Title zorunludur. Title bilgisini lütfen doldurunuz.";
                    return Return;
                }

                if (string.IsNullOrEmpty(request.Address))
                {
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "Address.is.empty";
                    Return.Meta.ErrorMessage = "Address zorunludur. Address bilgisini lütfen doldurunuz.";
                    return Return;
                }

                if (string.IsNullOrEmpty(request.Email))
                {
                    Return.Meta.IsSuccess = false;
                    Return.Meta.Error = "Email.is.empty";
                    Return.Meta.ErrorMessage = "Email zorunludur. Email bilgisini lütfen doldurunuz.";
                    return Return;
                }

                request.CreatedDate = DateTime.Now;
                _context.Customers.Add(request);
                _context.SaveChanges();

                Return.Entity = request;
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
