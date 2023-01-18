using DataAccess.Concrete;
using DataAccess.Concrete.Repositories;
using Entity.Models;
using Entity.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager 
    {
        private readonly GenericRepository<Customer> _repository;

        public CustomerManager(InvoiceAppDbContext context)
        {
            _repository = new GenericRepository<Customer>(context);
        }


        public Result<Customer> GetAll()
        {
            var Return = new Result<Customer>() { Meta = new Meta() };
            try
            {
                Return.Entities = _repository.List();
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
                _repository.Insert(request);

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
