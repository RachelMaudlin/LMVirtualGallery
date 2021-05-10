using LMVirtualGallery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMVirtualGallery.Services
{
    public class EventService
    {
        private readonly Guid _userId;

        public EventService(Guid userId)
        {
            _userId = userId;
        }

      public bool CreateEvent(EventCreate model)
        {
            var entity =
                new Event()
                {
                    OwnerId = _userId,
                    NameOfEvent = model.NameOfEvent,
                    EventDescription = model.EventDescription,
                    EventDate = model.EventDate,
                    EventAddress = model.EventAddress
                };

            using(var ctx = new ApplicationDbContext())
            {
                ctx.Events.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<EventItems> GetEvents()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Events
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new EventItems
                        {
                            EventId = e.EventId,
                            NameOfEvent = e.NameOfEvent,
                            EventDescription = e.EventDescription
                        }
                     );
                return query.ToArray();
            }
        }
       
    }
}
