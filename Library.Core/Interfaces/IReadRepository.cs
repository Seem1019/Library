using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    /// <summary>
    /// Provides read operations for a generic type.
    /// </summary>
    /// <typeparam name="T">The type of objects to read.</typeparam>
    public interface IReadRepository<T> where T : class
    {
        /// <summary>
        /// Gets an object of type T by its ID.
        /// </summary>
        /// <param name="id">The ID of the object to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the object of type T.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Gets all objects of type T.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of objects of type T.</returns>
        Task<IEnumerable<T>> GetAllAsync();
    }
}
