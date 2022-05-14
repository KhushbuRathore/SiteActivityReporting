using System;

namespace SiteActivityReporting.DTO
{
    public class BaseDTO
    {
        public string Key { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
