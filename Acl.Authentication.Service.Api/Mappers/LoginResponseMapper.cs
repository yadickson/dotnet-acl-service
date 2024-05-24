using Acl.Authentication.Service.Api.Dtos;
using Acl.Authentication.Service.Domain.Models;

namespace Acl.Authentication.Service.Api.Mappers;

public sealed class LoginResponseMapper : ILoginResponseMapper
{
    public LoginResponseDto FromModelToDto(UserModel response)
    {
        throw new NotImplementedException();
    }
}