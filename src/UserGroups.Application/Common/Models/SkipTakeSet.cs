using System.Collections.Generic;

namespace UserGroups.Application.Common.Models
{
    public class SkipTakeSet<T>
    {
        public int Skipped { get; set; }
        public int Taken { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<T> Records { get; set; }
    }
}