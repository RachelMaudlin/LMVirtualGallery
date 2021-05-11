using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMVirtualGallery
{
    public class EventEdit
    {
        public int EventId { get; set; }
        [Display(Name = "Name of Event")]
        public string NameOfEvent { get; set; }
        [Display(Name = "Event Description")]
        public string EventDescription { get; set; }
        [Display(Name = "Event Address")]
        public string EventAddress { get; set; }
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }
    }
}
