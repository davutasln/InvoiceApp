using System;
using System.ComponentModel.DataAnnotations;

namespace Entity.Models
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
