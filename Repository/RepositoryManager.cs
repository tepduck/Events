using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IEventRepository _eventRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IEventRepository Event
        {
            get
            {
                if (_eventRepository == null)
                {
                    _eventRepository = new EventRepository(_repositoryContext);
                }

                return _eventRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
