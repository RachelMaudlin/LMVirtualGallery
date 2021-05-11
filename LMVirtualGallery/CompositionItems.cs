using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMVirtualGallery
{
    public class CompositionItems
    {
        public int CompositionId { get; set; }
        [Display(Name = "Name of Composition")]
        public string CompositionName { get; set; }
        [Display(Name = "Composition Medium")]
        public string CompositionMedium { get; set; }
    }
}
