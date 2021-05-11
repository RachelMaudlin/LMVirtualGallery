﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMVirtualGallery.Data.Entities
{
    public class Gallery
    {
        [Key]
        public int GalleryId { get; set; }

        public Guid OwnerId { get; set; }

        [ForeignKey(nameof(Exhibition))]
        public int ExhibitionId { get; set; }
        public virtual Exhibition Exhibition { get; set; }

        [ForeignKey(nameof(Composition))]
        public int CompositionId { get; set; }
        public virtual Composition Composition { get; set; }
    }
}
