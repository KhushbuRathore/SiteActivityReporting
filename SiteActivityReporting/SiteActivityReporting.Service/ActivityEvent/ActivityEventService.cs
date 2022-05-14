using SiteActivityReporting.Common;
using SiteActivityReporting.Data;
using SiteActivityReporting.Domain;
using System;
using System.Linq;

namespace SiteActivityReporting.Service
{
    public class ActivityEventService : IActivityEventService
    {
        #region props

        private readonly Db _dbContext;

        #endregion

        #region ctor

        public ActivityEventService()
        {
            _dbContext = new Db();
        }

        #endregion

        #region methods

        public void InsertActivityEvent(ActivityEvent activityEvent)
        {
            _dbContext.ActivityEventList.Add(activityEvent);
        }

        public int GetActivityEventTotal(string key)
        {
           var pruneDateTime = CommonHelper.PruneDateTime(13);

           var grpActivityEvent = _dbContext.ActivityEventList.Where(x => x.IsDeleted != true && x.Key.ToLower() == key.ToLower() && x.CreatedOn > pruneDateTime).GroupBy(x => x.Key).Select(y => new {
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