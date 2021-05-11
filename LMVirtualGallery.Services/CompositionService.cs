using LMVirtualGallery.Data;
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
                    CompositionCreation = model.CompositionCreation
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
    }
}
