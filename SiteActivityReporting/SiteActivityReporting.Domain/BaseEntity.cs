using System;

namespace SiteActivityReporting.Domain
{
    public class BaseEntity
    {

        public string Key { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsDeleted { get; set; } 
    }
}
