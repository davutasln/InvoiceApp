using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class InvoiceLine : BaseEntity
    {
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
