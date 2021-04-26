using System;

namespace Application.ViewModels.ActivityDto
{
    public class ActivityItem
    {
        public Guid ActivityId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public string ImagePath { get; set; }
        public DateTime HoldingDate { get; set; }
        public string CategoryName { get; set; }
        public string HostName { get; set; }

        public int MonthDiff { get; set; }



    }
}
