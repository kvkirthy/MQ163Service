using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MQ163Service
{
    public class CommonEventsHelper
    {
        internal static void WriteToEventLog(string message, EventLogEntryType entryType)
        {
            try
            {
                EventLog.WriteEntry("MQ163", message, entryType);
            }
            catch
            {
                // failure to write to event log should just not crash the service.
            }
        }
    }
}