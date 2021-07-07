using System;
using System.Collections.Generic;

namespace Wimc.Infrastructure.Logging
{
    public class LogDetail
    {
        public LogDetail()
        {
            TimeStamp = DateTime.Now;
            AdditionalInfo = new Dictionary<string, object>();
        }

        public DateTime TimeStamp { get; private set; }

        public string Message { get; set; }

        public string Location { get; set; }

        public string Hostname { get; set; }

        public string Application { get; set; }

        public string UserId { get; set; }

        public long? ElapsedMilliseconds { get; set; }

        public Exception Exception { get; set; }

        public string CorrelationId { get; set; }

        public Dictionary<string, object> AdditionalInfo { get; set; }

    }
    
}