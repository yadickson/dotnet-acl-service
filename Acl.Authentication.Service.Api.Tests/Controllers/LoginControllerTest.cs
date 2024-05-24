using System.Text.Json;
using Acl.Authentication.Service.Api.Controllers;
using Acl.Authentication.Service.Api.Dtos;
using Acl.Authentication.Service.Api.Services;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using MyTested.AspNetCore.Mvc;
using HttpMethod = MyTested.AspNetCore.Mvc.HttpMethod;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace Acl.Authentication.Service.Api.Tests.Controllers;

[TestClass]
public sealed class LoginControllerTest
{
    private readonly LoginController _controller;
    private readonly Faker _faker;
    private readonly Mock<ILogger<LoginController>> _loggerMock;
    private readonly Mock<ILoginService> _serviceMock;

    public LoginControllerTest()
    {
        _faker = new Faker();
        _loggerMock = new Mock<ILogger<LoginController>>();
        _serviceMock = new Mock<ILoginService>();
        _controller = new LoginController(_loggerMock.Object, _serviceMock.Object);
    }

    [TestMethod]
    public async Task Should_Check_Logger_Is_Called_With_Message()
    {
        var username = _faker.Random.Word();

        var request = new Faker<LoginRequestDto>()
            .RuleFor(o => o.Username, username)
            .Generate();

        var expected = $"Authentication by username [{username}]";

        await _controller.Login(request);

        _loggerMock.Verify(method => method.Log(
                It.Is<LogLevel>(level => level == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((message, _) => expected.CompareTo(message.ToString()) == 0),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((_, __) => true)),
            Times.Once());
    }

    [TestMethod]
    public async Task Should_Check_Return_Authenticate_Response()
    {
        var request = new Faker<LoginRequestDto>().Generate();
        var expected = new Faker<LoginResponseDto>().Generate();

        _serviceMock.Setup(method => method.Authenticate(It.IsAny<LoginRequestDto>())).ReturnsAsync(expected);

        var response = await _controller.Login(request);

        Assert.IsNotNull(response);
        Assert.IsTrue(response is ObjectResult);
        Assert.AreSame(expected, ((ObjectResult)response).Value);
    }

    [TestMethod]
    public async Task Should_Check_Call_Authenticate_One_Time()
    {
        var request = new Faker<LoginRequestDto>().Generate();
        var expected = new Faker<LoginResponseDto>().Generate();

        _serviceMock.Setup(method => method.Authenticate(It.IsAny<LoginRequestDto>())).ReturnsAsync(expected);

        await _controller.Login(request);

        _serviceMock.Verify(method => method.Authenticate(request), Times.Once());
    }

    [TestMethod]
    public async Task Should_Check_Return_Authenticate_Status_Code()
    {
        var request = new Faker<LoginRequestDto>().Generate();
        var expected = new Faker<LoginResponseDto>().Generate();

        _serviceMock.Setup(method => method.Authenticate(It.IsAny<LoginRequestDto>())).ReturnsAsync(expected);

        var response = await _controller.Login(request);

        Assert.IsNotNull(response);
        Assert.IsTrue(response is IStatusCodeActionResult);
        Assert.AreEqual((int)HttpStatusCode.OK, ((IStatusCodeActionResult)response).StatusCode);
    }

    [TestMethod]
    public void Should_Check_Login_By_Route_With_Empty_Body()
    {
        var json = "{}";
        var expected = new LoginRequestDto();

        MyRouting
            .Configuration()
            .ShouldMap(request => request
                .WithMethod(HttpMethod.Post)
                .WithLocation("/login/authenticate")
                .WithStringBody(json)
                .WithContentType("application/json")
            )
            .To<LoginController>(controller => controller.Login(expected));
    }

    [TestMethod]
    public void Should_Check_Login_By_Route_With_Full_Faker_Body()
    {
        var requestObject = new Faker<LoginRequestDto>()
            .RuleForType(typeof(string), f => f.Lorem.Text())
            .Generate();

        var json = JsonSerializer.Serialize(requestObject);

        MyRouting
            .Configuration()
            .ShouldMap(request => request
                .WithMethod(HttpMethod.Post)
                .WithLocation("/login/authenticate")
                .WithStringBody(json)
                .WithContentType("application/json")
            )
            .To<LoginController>(controller => controller.Login(requestObject));
    }

    [TestMethod]
    public void Should_Check_Login_By_Route_With_Minimal_Body_with_camel_Case()
    {
        var username = _faker.Random.AlphaNumeric(20);
        var password = _faker.Random.AlphaNumeric(20);

        var json = @"{
        ""username"": """ + username + @""",
        ""password"": """ + password + @"""}";

        var expected = new LoginRequestDto { Username = username, Password = password };

        MyRouting
            .Configuration()
            .ShouldMap(request => request
                .WithMethod(HttpMethod.Post)
                .WithLocation("/login/authenticate")
                .WithStringBody(json)
                .WithContentType("application/json")
            )
            .To<LoginController>(controller => controller.Login(expected));
    }

    [TestMethod]
    public void Should_Check_Login_By_Route_With_Minimal_Body_with_Pascal_Case()
    {
        var username = _faker.Random.AlphaNumeric(20);
        var password = _faker.Random.AlphaNumeric(20);

        var json = @"{
        ""Username"": """ + username + @""",
        ""Password"": """ + password + @"""}";

        var expected = new LoginRequestDto { Username = username, Password = password };

        MyRouting
            .Configuration()
            .ShouldMap(request => request
                .WithMethod(HttpMethod.Post)
                .WithLocation("/login/authenticate")
                .WithStringBody(json)
                .WithContentType("application/json")
            )
            .To<LoginController>(controller => controller.Login(expected));
    }
}