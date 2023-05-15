using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    /// <summary>
    /// Provides write operations for a generic type.
    /// </summary>
    /// <typeparam name="T">The type of objects to write.</typeparam>
    public interface IWriteRepository<T> where T : class
    {
        /// <summary>
        /// Adds a new object of type T.
        /// </summary>
        /// <param name="entity">The object to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an existing object of type T.
        /// </summary>
        /// <param name="entity">The object to update.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an existing object of type T.
        /// </summary>
        /// <param name="entity">The object to delete.</param>
        void Delete(T entity);
    }
}
