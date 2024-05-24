using Acl.Authentication.Service.Api.Dtos;
using Acl.Authentication.Service.Api.Mappers;
using Acl.Authentication.Service.Domain.UseCases;
using Microsoft.Extensions.Logging;

namespace Acl.Authentication.Service.Api.Services;

public sealed class LoginService(
    ILogger<LoginService> logger,
    ILoginRequestMapper requestMapper,
    ILoginUseCase useCase,
    ILoginResponseMapper responseMapper) : ILoginService
{
    public async Task<LoginResponseDto> Authenticate(LoginRequestDto? request)
    {
        logger.LogInformation("Authentication user");
        var requestModel = requestMapper.FromDtoToModel(request);
        var responseModel = await useCase.Execute(requestModel);
        return responseMapper.FromModelToDto(responseModel);
    }
}