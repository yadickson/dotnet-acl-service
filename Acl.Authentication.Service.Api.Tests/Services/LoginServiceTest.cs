using Acl.Authentication.Service.Api.Dtos;
using Acl.Authentication.Service.Api.Mappers;
using Acl.Authentication.Service.Api.Services;
using Acl.Authentication.Service.Domain.Models;
using Acl.Authentication.Service.Domain.UseCases;
using Bogus;
using Microsoft.Extensions.Logging;
using Moq;

namespace Acl.Authentication.Service.Api.Tests.Services;

[TestClass]
public sealed class LoginServiceTest
{
    private readonly Mock<ILogger<LoginService>> _loggerMock;
    private readonly Mock<ILoginRequestMapper> _requestMapper;
    private readonly Mock<ILoginResponseMapper> _responseMapper;
    private readonly ILoginService _service;
    private readonly Mock<ILoginUseCase> _useCase;

    public LoginServiceTest()
    {
        _loggerMock = new Mock<ILogger<LoginService>>();
        _requestMapper = new Mock<ILoginRequestMapper>();
        _useCase = new Mock<ILoginUseCase>();
        _responseMapper = new Mock<ILoginResponseMapper>();

        _service = new LoginService(_loggerMock.Object, _requestMapper.Object, _useCase.Object, _responseMapper.Object);
    }

    [TestMethod]
    public async Task Should_Check_Logger_Is_Called_With_Messages()
    {
        var request = new Faker<LoginRequestDto>().Generate();
        var expected = "Authentication user";

        await _service.Authenticate(request);

        _loggerMock.Verify(method => method.Log(
                It.Is<LogLevel>(level => level == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((message, _) => expected.CompareTo(message.ToString()) == 0),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((_, __) => true)),
            Times.Once());
    }

    [TestMethod]
    public async Task Should_Check_In_Order_Call_Dependencies()
    {
        var request = new Faker<LoginRequestDto>().Generate();
        var requestModel = new Faker<LoginModel>().Generate();
        var responseModel = new Faker<UserModel>().Generate();

        var callOrder = 0;

        _loggerMock.Setup(method => method.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()
            ))
            .Callback(() => Assert.AreEqual(1, ++callOrder));

        _requestMapper.Setup(method => method.FromDtoToModel(It.IsAny<LoginRequestDto>()))
            .Callback(() => Assert.AreEqual(2, ++callOrder));

        _useCase.Setup(method => method.Execute(It.IsAny<LoginModel>()))
            .Callback(() => Assert.AreEqual(3, ++callOrder));

        _responseMapper.Setup(method => method.FromModelToDto(It.IsAny<UserModel>()))
            .Callback(() => Assert.AreEqual(4, ++callOrder));

        await _service.Authenticate(request);
    }

    [TestMethod]
    public async Task Should_Check_Request_Mapper_Parameters()
    {
        var request = new Faker<LoginRequestDto>().Generate();

        await _service.Authenticate(request);

        _requestMapper.Verify(method => method.FromDtoToModel(request), Times.Once());
    }

    [TestMethod]
    public async Task Should_Check_Execute_Use_Case_Parameters()
    {
        var request = new Faker<LoginRequestDto>().Generate();
        var requestModel = new Faker<LoginModel>().Generate();

        var sequence = new MockSequence();

        _requestMapper.Setup(method => method.FromDtoToModel(It.IsAny<LoginRequestDto>())).Returns(requestModel);

        await _service.Authenticate(request);

        _useCase.Verify(method => method.Execute(requestModel), Times.Once());
    }

    [TestMethod]
    public async Task Should_Check_Response_Mapper_Parameters()
    {
        var request = new Faker<LoginRequestDto>().Generate();
        var responseModel = new Faker<UserModel>().Generate();

        var sequence = new MockSequence();

        _useCase.Setup(method => method.Execute(It.IsAny<LoginModel>())).ReturnsAsync(responseModel);

        await _service.Authenticate(request);

        _responseMapper.Verify(method => method.FromModelToDto(responseModel), Times.Once());
    }

    [TestMethod]
    public async Task Should_Check_Return_Response()
    {
        var request = new Faker<LoginRequestDto>().Generate();
        var expected = new Faker<LoginResponseDto>().Generate();

        _responseMapper.Setup(method => method.FromModelToDto(It.IsAny<UserModel>())).Returns(expected);

        var response = await _service.Authenticate(request);

        Assert.IsNotNull(response);
        Assert.AreSame(expected, response);
    }
}