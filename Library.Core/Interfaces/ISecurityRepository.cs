using Library.Core.Entities;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface ISecurityRepository : IRepository<Security>
        {
            Task<Security> GetLoginByCredentials(UserLogin login);
        }
    
}
