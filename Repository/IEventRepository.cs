using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEventsAsync(bool trackChanges);
        Task<Event> GetEventAsync(Guid id, bool trackChanges);
        void CreateEvent (Event customEvent);
        void DeleteEvent(Event customEvent);

    }
}
