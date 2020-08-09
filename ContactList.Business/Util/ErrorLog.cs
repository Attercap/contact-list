using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ContactList.Business
{
    /// <summary>
    /// Class to log errors
    /// Simple right now, but could be expanded to include severity notations, alerts, etc.
    /// </summary>
    public static class ErrorLog
    {
        /// <summary>
        /// Writes error to event log--could be updated to write to log file(s)
        /// </summary>
        /// <param name="ex">System Exception</param>
        public static void LogError(Exception ex)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "ClientList";
                eventLog.WriteEntry(ex.Message, EventLogEntryType.Error, 101, 1);
            }
        }
    }
}
