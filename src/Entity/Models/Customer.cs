using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Models
{
    public class Customer : BaseEntity
    {
        public string TaxNumber { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
