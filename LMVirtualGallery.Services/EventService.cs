using LMVirtualGallery.Data;
using LMVirtualGallery.Data.Entities;
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

        public EventDetail GetEventById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Events
                        .Single(e => e.EventId == id && e.OwnerId == _userId);
                return
                    new EventDetail
                    {
                        EventId = entity.EventId,
                        NameOfEvent = entity.NameOfEvent,
                        EventDescription = entity.EventDescription,
                        EventDate = entity.EventDate,
                        EventAddress = entity.EventAddress
                    };
                   
            }
        }

        public bool UpdateEvent(EventEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Events
                        .Single(e => e.EventId == model.EventId && e.OwnerId == _userId);

                entity.NameOfEvent = model.NameOfEvent;
                entity.EventDescription = model.EventDescription;
                entity.EventDate = model.EventDate;
                entity.EventAddress = model.EventAddress;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteEvent(int eventId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Events
                        .Single(e => e.EventId == eventId && e.OwnerId == _userId);
                ctx.Events.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
       
    }
}
