using Acl.Authentication.Service.Api.Dtos;
using Acl.Authentication.Service.Domain.Models;

namespace Acl.Authentication.Service.Api.Mappers;

public interface ILoginResponseMapper
{
    LoginResponseDto FromModelToDto(UserModel response);
}