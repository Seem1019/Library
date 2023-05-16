using Library.Core.Entities;
using Library.Core.Exceptions;
using Library.Core.Interfaces;
using Library.Core.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTest
{
    public class SecurityServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly SecurityService _securityService;

        public SecurityServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _securityService = new SecurityService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetLoginByCredentials_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var userLogin = new UserLogin
            {
                User = "testuser",
                Password = "password"
            };

            var expectedUser = new Security
            {
                Id = 1,
                User = "testuser"
                // Set other properties as needed
            };

            _unitOfWorkMock.Setup(u => u.SecurityRepository.GetLoginByCredentials(userLogin))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _securityService.GetLoginByCredentials(userLogin);

            // Assert
            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public async Task GetLoginByCredentials_InvalidCredentials_ThrowsBusinessException()
        {
            // Arrange
            var userLogin = new UserLogin
            {
                User = "testuser",
                Password = "password"
            };

            _unitOfWorkMock.Setup(u => u.SecurityRepository.GetLoginByCredentials(userLogin))
                .ReturnsAsync((Security)null);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => _securityService.GetLoginByCredentials(userLogin));
        }

        [Fact]
        public async Task RegisterUser_ValidSecurity_ReturnsUserId()
        {
            // Arrange
            var security = new Security
            {
                User = "testuser",
                Password = "password"
                // Set other properties as needed
            };

            var expectedUserId = 1;

            _unitOfWorkMock.Setup(u => u.SecurityRepository.AddAsync(security))
                .Verifiable();
            _unitOfWorkMock.Setup(u => u.CommitAsync())
                .ReturnsAsync(expectedUserId);

            // Act
            var result = await _securityService.RegisterUser(security);

            // Assert
            Assert.Equal(expectedUserId, result);
            _unitOfWorkMock.Verify(u => u.SecurityRepository.AddAsync(security), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
