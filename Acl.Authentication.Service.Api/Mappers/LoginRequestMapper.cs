using Acl.Authentication.Service.Api.Dtos;
using Acl.Authentication.Service.Domain.Models;

namespace Acl.Authentication.Service.Api.Mappers;

public sealed class LoginRequestMapper : ILoginRequestMapper
{
    public LoginModel FromDtoToModel(LoginRequestDto? request)
    {
        throw new NotImplementedException();
    }
}