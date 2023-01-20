using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Extensions
{
    public static class DateProcess
    {
        public static DateTime GenerateRandomDate()
        {
            Random gen = new Random();
            DateTime start = new DateTime(2000, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
