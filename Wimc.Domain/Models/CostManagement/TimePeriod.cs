using System;

namespace Wimc.Domain.Models.CostManagement
{
    public class TimePeriod
    {
        public TimePeriod()
        {
            
        }

        public TimePeriod(DateTime from, DateTime to)
        {
            From = from.ToString("d");
            To = to.ToString("d");
        }
        
        public string From { get; set; }
        public string  To { get; set;  }
    }
}