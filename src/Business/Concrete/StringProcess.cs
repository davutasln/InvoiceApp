using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public static class StringProcess
    {
        public static string GenerateInvoiceNuumber()
        {
            Random random = new Random();
            return $"{DateTime.Now.Year}{random.Next(100000000, int.MaxValue)}";
        }
    }
}
