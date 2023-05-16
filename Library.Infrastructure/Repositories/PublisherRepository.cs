using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(TravelLibraryContext context) : base(context)
        {
        }
    }
}
