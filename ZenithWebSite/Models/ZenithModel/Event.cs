using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZenithWebSite.Models.Validation;

namespace ZenithWebSite.Models.ZenithModel
{
    public class Event
    {
        public int EventId { get; set; }

        [Display(Name = "Starting Date and Time")]
        public DateTime StartTime { get; set; }

        [DateGreater("StartTime", ErrorMessage = "Ending time cannot be before Starting time")]
        [Display(Name = "Ending Date and Time")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Creator")]
        public string CreatorName { get; set; }

        [Display(Name = "Date Created")]
        [HiddenInput(DisplayValue = true)]

        public DateTime CreationDate { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Activity Category")]
        public int ActivityCategoryId { get; set; }
        public ActivityCategory ActivityCategory { get; set; }
    }
}
