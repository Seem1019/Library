namespace Library.Core.Interfaces
{
    /// <summary>
    /// Provides read and write operations for a generic type.
    /// </summary>
    /// <typeparam name="T">The type of objects to read and write.</typeparam>
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class { }
}
