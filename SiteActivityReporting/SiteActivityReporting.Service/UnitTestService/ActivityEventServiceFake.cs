using SiteActivityReporting.Common;
using SiteActivityReporting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteActivityReporting.Service
{
    public class ActivityEventServiceFake : IActivityEventService
    {
        #region props

        private readonly IList<ActivityEvent> _activityEventList;

        #endregion

        #region ctor

        public ActivityEventServiceFake()
        {
            _activityEventList = new List<ActivityEvent>()
            {
                new ActivityEvent { Key = "learn_more_page", IsDeleted = false, CreatedOn = DateTime.Now.AddHours(-12), ModifiedOn = DateTime.Now.AddHours(-12), Value = 5 },
                new ActivityEvent { Key = "learn_more_page", IsDeleted = false, CreatedOn = DateTime.Now.AddHours(-11), ModifiedOn = DateTime.Now.AddHours(-11), Value = 32 },
                new ActivityEvent { Key = "learn_more_page", IsDeleted = false, CreatedOn = DateTime.Now.AddHours(-13), ModifiedOn = DateTime.Now.AddHours(-13), Value = 90 },
                new ActivityEvent { Key = "learn_more_page", IsDeleted = false, CreatedOn = DateTime.Now.AddHours(-14), ModifiedOn = DateTime.Now.AddHours(-14), Value = 30 }
            };
        }

        #endregion

        #region methods

        public void InsertActivityEvent(ActivityEvent activityEvent)
        {
            _activityEventList.Add(activityEvent);
        }

        public int GetActivityEventTotal(string key)
        {
            var pruneDateTime = CommonHelper.PruneDateTime(13);
            var grpActivityEvent = _activityEventList.Where(x => x.IsDeleted != true && x.Key.ToLower() == key.ToLower() && x.CreatedOn > pruneDateTime).GroupBy(x => x.Key).Select(y => new
            {
                Id = y.Key,
                Total = y.Sum(x => x.Value)
            }).FirstOrDefault();
            if (grpActivityEvent != null)
            {
                return grpActivityEvent.Total;
            }
            return 0;
        }

        #endregion
    }
}
