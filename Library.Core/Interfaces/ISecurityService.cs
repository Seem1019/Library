using Library.Core.Entities;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin userLogin);
        Task<int>RegisterUser(Security security);
    }
}
