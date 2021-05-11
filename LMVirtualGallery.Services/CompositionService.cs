using LMVirtualGallery.Data;
using LMVirtualGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMVirtualGallery.Services
{
    public class CompositionService
    {
        private readonly Guid _userId;

        public CompositionService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComposition(CompositionCreate model)
        {
            var entity =
                new Composition()
                {
                    OwnerId = _userId,
                    CompositionName = model.CompositionName,
                    CompositionMedium = model.CompositionMedium,
                    CompositionDescription = model.CompositionDescription,
                    CompositionCreation = model.CompositionCreation,
                    ImageName = model.ImageName
                };

            using(var ctx = new ApplicationDbContext())
            {
                ctx.Compositions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CompositionItems> GetCompositions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Compositions
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                 new CompositionItems
                                 {
                                     CompositionId = e.CompositionId,
                                     CompositionMedium = e.CompositionMedium,
                                     CompositionName = e.CompositionName
                                 }
                                );
                return query.ToArray();
            }
        }

        public CompositionDetail GetCompositionById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Compositions
                        .Single(e => e.CompositionId == id && e.OwnerId == _userId);
                return
                    new CompositionDetail
                    {
                        CompositionId = entity.CompositionId,
                        CompositionName = entity.CompositionName,
                        ImageName = entity.ImageName,
                        CompositionDescription = entity.CompositionDescription,
                        CompositionMedium = entity.CompositionMedium,
                        CompositionCreation = entity.CompositionCreation
                    };
            }
        }

        public bool UpdateComposition(CompositionEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Compositions
                        .Single(e => e.CompositionId == model.CompositionId && e.OwnerId == _userId);

                entity.CompositionName = model.CompositionName;
                entity.ImageName = model.ImageName;
                entity.CompositionMedium = model.CompositionMedium;
                entity.CompositionDescription = model.CompositionDescription;
                entity.CompositionCreation = model.CompositionCreation;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteComposition(int compositionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Compositions
                        .Single(e => e.CompositionId == compositionId && e.OwnerId == _userId);

                ctx.Compositions.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
