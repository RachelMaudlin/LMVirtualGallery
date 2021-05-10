using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMVirtualGallery
{
    public class ExhibitionEdit
    {
        public int ExhibitionId { get; set; }
        [Display(Name = "Name of Exhibition")]
        public string ExhibitionName { get; set; }
        [Display(Name = "Exhibition Description")]
        public string ExhibitionDescription { get; set; }
        [Display(Name = "Exhibition Date")]
        public string ExhibitionDate { get; set; }
        [Display(Name = "Exhibition Location")]
        public string ExhibitionLocation { get; set; }
    }
}
