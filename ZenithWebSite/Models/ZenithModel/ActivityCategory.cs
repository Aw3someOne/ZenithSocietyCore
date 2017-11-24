using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebSite.Models.ZenithModel
{
    public class ActivityCategory
    {
        public int ActivityCategoryId { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string ActivityDescription { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = true)]
        public DateTime CreationDate { get; set; }

        public List<Event> Events { get; set; }
    }
}
