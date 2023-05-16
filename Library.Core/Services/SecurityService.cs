using Library.Core.Entities;
using Library.Core.Exceptions;
using Library.Core.Interfaces;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Security> GetLoginByCredentials(UserLogin userLogin)
        {
            var user = await _unitOfWork.SecurityRepository.GetLoginByCredentials(userLogin);
            if (user == null)
            {
                throw new BusinessException("User or password invalid.");
            }
            return user;
        }

        public async Task<int> RegisterUser(Security security)
        {
            await _unitOfWork.SecurityRepository.AddAsync(security);
            return await _unitOfWork.CommitAsync();
        }
    }
}
