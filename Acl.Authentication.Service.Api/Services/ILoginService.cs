using Acl.Authentication.Service.Api.Dtos;

namespace Acl.Authentication.Service.Api.Services;

public interface ILoginService
{
    Task<LoginResponseDto> Authenticate(LoginRequestDto? request);
}