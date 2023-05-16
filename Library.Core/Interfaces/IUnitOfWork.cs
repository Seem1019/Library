using System;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    /// <summary>
    /// Defines an interface for a Unit of Work - a concept that allows for the orchestration of multiple related
    /// operations into a single atomic operation, ensuring data integrity.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Provides access to the Author repository.
        /// </summary>
        IAuthorRepository Authors { get; }

        /// <summary>
        /// Provides access to the Book repository.
        /// </summary>
        IBookRepository Books { get; }

        /// <summary>
        /// Provides access to the Publisher repository.
        /// </summary>
        IPublisherRepository Publishers { get; }


        /// <summary>
        /// Provides access to the AuthorHasBooks repository.
        /// </summary>
        IAuthorHasBookRepository AuthorHasBooks { get; }

        // <summary>
        /// Commits all changes made within this unit of work.
        /// </summary>
        /// <returns>The number of changes written to the database.</returns>
        Task<int> CommitAsync();
        ISecurityRepository SecurityRepository { get; }

    }
}
