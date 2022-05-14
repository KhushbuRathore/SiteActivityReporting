using SiteActivityReporting.Domain;
using System.Collections.Generic;

namespace SiteActivityReporting.Service
{
    public interface IActivityEventService
    {
        void InsertActivityEvent(ActivityEvent activityEvent);
        int GetActivityEventTotal(string key);
    }
}