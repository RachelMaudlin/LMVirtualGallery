using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMVirtualGallery
{
    public class ExhibitionItems
    {
        public int ExhibitionId { get; set; }
        [Required]
        [Display(Name = "Name of Exhibition")]
        public string ExhibitionName { get; set; }
        [Required]
        [Display(Name = "Exhibition Description")]
        public string ExhibitionDescription { get; set; }
    }
}
