using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEventsAsync(bool trackChanges = false);
        Task<Event> GetEventAsync(Guid id, bool trackChanges = false);
        void CreateEvent(Event customEvent);
        void DeleteEvent(Event customEvent);

    }
}
