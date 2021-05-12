using LMVirtualGallery.Data;
using LMVirtualGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMVirtualGallery.Services
{
    public class GalleryService
    {
        private readonly Guid _userId;

        public GalleryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateGallery(GalleryCreate model)
        {
            var entity =
                new Gallery()
                {
                    OwnerId = _userId,
                    CompositionId = model.CompositionId,
                    ExhibitionId = model.ExhibitionId
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Galleries.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<GalleryList> GetGalleries()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Galleries
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new GalleryList
                        {
                            GalleryId = e.GalleryId,
                            Composition = e.Composition,
                            Exhibition = e.Exhibition
                        }
                        );
                return query.ToArray();
            }
        }

        public GalleryDetail GetGalleryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Galleries
                        .Single(e => e.GalleryId == id && e.OwnerId == _userId);
                return
                    new GalleryDetail
                    {
                        GalleryId = entity.GalleryId,
                        Composition = entity.Composition,
                        Exhibition = entity.Exhibition
                    };
            }
        }

        //public bool UpdateExhibition(ExhibitionEdit model)
        //{
            //using (var ctx = new ApplicationDbContext())
            //{
                //var entity =
                   // ctx
                        //.Exhibitions
                        //.Single(e => e.ExhibitionId == model.ExhibitionId && e.OwnerId == _userId);

                //entity.ExhibitionName = model.ExhibitionName;
                //entity.ExhibitionDescription = model.ExhibitionDescription;
                //entity.ExhibitionDate = model.ExhibitionDate;
                //entity.ExhibitionLocation = model.ExhibitionLocation;

                //return ctx.SaveChanges() == 1;
           // }
       

        public bool DeleteGallery(int galleryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Galleries
                        .Single(e => e.GalleryId == galleryId && e.OwnerId == _userId);

                ctx.Galleries.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
