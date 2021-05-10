using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMVirtualGallery
{
    public class EventItems
    {
        [Display(Name ="Event Number")]
        public int EventId { get; set; }
        [Required]
        [Display(Name = "Name of Event")]
        public string NameOfEvent { get; set; }
        [Required]
        [Display(Name = "Event Description")]
        public string EventDescription { get; set; }
    }
}
