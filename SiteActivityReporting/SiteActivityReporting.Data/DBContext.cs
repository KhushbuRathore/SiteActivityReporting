using SiteActivityReporting.Domain;
using System.Collections.Generic;

namespace SiteActivityReporting.Data
{
    public class Db
    {
        public readonly IList<ActivityEvent> ActivityEventList;

        public Db()
        {
            ActivityEventList = new List<ActivityEvent>();
        }
    }
}
