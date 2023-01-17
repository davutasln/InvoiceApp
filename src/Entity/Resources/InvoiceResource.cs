using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Resources
{
    public class InvoiceResource 
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
