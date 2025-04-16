using Xunit;
using Moq;
using AuthExcelService.WebApp.Controllers.Account;
using AuthExcelService.Services.IRepository;
using AuthExcelService.Services.Models;
using AuthExcelService.Services.Models.ResponseModel;
using Microsoft.Extensions.Logging;
using AutoMapper;

public class AuthControllerTests
{
    private readonly Mock<IAuthWebService> _authWebServiceMock;
    private readonly Mock<ILogger<AuthController>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _authWebServiceMock = new Mock<IAuthWebService>();
        _loggerMock = new Mock<ILogger<AuthController>>();
        _mapperMock = new Mock<IMapper>();
        _authController = new AuthController(_mapperMock.Object, _loggerMock.Object, _authWebServiceMock.Object);
    }

    [Fact]
    public async Task Login_ShouldReturnError_WhenDeserializationFails()
    {
        // Arrange
        var loginViewModel = new LoginViewModel { UserEmail = "test@example.com" };
        var apiResponse = new ApiResponseWeb
        {
            IsSuccess = true,
            Result = null // Simulate null result
        };

        _authWebServiceMock
            .Setup(service => service.LoginAsync(It.IsAny<LoginModel>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _authController.Login(loginViewModel);

        // Assert
        Assert.NotNull(result);
        _loggerMock.Verify(logger => logger.LogError(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
    }
}
