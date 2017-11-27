using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebSite.Models.ZenithModel
{
    public class EventDTO
    {
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string ActivityDescription { get; set; }

        public EventDTO(Event ev)
        {
            EventDate = ev.StartTime.Date;
            StartTime = ev.StartTime.TimeOfDay;
            EndTime = ev.EndTime.TimeOfDay;
            if (ev.ActivityCategory != null)
            {
                ActivityDescription = ev.ActivityCategory.ActivityDescription;
            }
        }
    }
}
