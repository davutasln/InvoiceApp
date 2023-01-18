using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Resources
{
    public class InvoiceSaveResource
    {
        public int CustomerId { get; set; }

        public decimal TotalAmount { get; set; }

        public List<InvoiceSaveLineResource> InvoiceLines { get; set; }
    }

    public class InvoiceSaveLineResource
    {
        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
