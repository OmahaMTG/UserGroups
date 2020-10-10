namespace UserGroups.Application.Common.Models
{
    public class SkipTakeRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; } = 10;
    }
}