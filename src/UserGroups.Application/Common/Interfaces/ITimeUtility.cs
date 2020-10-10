using System;
using System.Collections.Generic;

namespace UserGroups.Application.Common.Interfaces
{
    public interface ITimeUtility
    {
        DateTime GetCurrentSystemTime => DateTime.Now;
        IEnumerable<string> SearchTimeZones();
        DateTime GetCurrentTimeZoneTime(string timeZone);
    }
}