using LMVirtualGallery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMVirtualGallery.Services
{
    public class ExhibitionService
    {
        private readonly Guid _userId;

        public ExhibitionService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateExhibition(ExhibitionCreate model)
        {
            var entity =
                new Exhibition()
                {
                    OwnerId = _userId,
                    ExhibitionName = model.ExhibitionName,
                    ExhibitionDescription = model.ExhibitionDescription,
                    ExhibitionDate = model.ExhibitionDate,
                    ExhibitionLocation = model.ExhibitionLocation
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Exhibitions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ExhibitionItems> GetExhibitions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Exhibitions
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new ExhibitionItems
                        {
                            ExhibitionId = e.ExhibitionId,
                            ExhibitionName = e.ExhibitionName,
                            ExhibitionDescription = e.ExhibitionDescription
                        }
                        );
                return query.ToArray();
            }
        }

        public ExhibitionDetail GetExhibitionById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Exhibitions
                        .Single(e => e.ExhibitionId == id && e.OwnerId == _userId);
                return
                    new ExhibitionDetail
                    {
                        ExhibitionId = entity.ExhibitionId,
                        ExhibitionName = entity.ExhibitionName,
                        ExhibitionDescription = entity.ExhibitionDescription,
                        ExhibitionDate = entity.ExhibitionDate,
                        ExhibitionLocation = entity.ExhibitionLocation
                    };
            }
        }

        public bool UpdateExhibition(ExhibitionEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Exhibitions
                        .Single(e => e.ExhibitionId == model.ExhibitionId && e.OwnerId == _userId);

                entity.ExhibitionName = model.ExhibitionName;
                entity.ExhibitionDescription = model.ExhibitionDescription;
                entity.ExhibitionDate = model.ExhibitionDate;
                entity.ExhibitionLocation = model.ExhibitionLocation;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteExhibition(int exhibitionId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Exhibitions
                        .Single(e => e.ExhibitionId == exhibitionId && e.OwnerId == _userId);

                ctx.Exhibitions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
