using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateEvent(Event customEvent) => Create(customEvent);

        public void DeleteEvent(Event customEvent) => Delete(customEvent);

        public async Task<Event> GetEventAsync(Guid id, bool trackChanges) =>
            await GetByCondition(e => e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Event>> GetEventsAsync(bool trackChanges) =>
            await GetAll(trackChanges).ToListAsync();
    }
}
