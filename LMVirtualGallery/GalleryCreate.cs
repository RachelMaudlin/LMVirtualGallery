﻿using LMVirtualGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMVirtualGallery
{
    public class GalleryCreate
    {

        [ForeignKey(nameof(Exhibition))]
        public int ExhibitionId { get; set; }
        public virtual Exhibition Exhibition { get; set; }

        [ForeignKey(nameof(Composition))]
        public int CompositionId { get; set; }
        public virtual Composition Composition { get; set; }
    }
}
