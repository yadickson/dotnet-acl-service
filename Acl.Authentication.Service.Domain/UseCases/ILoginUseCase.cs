using Acl.Authentication.Service.Domain.Models;

namespace Acl.Authentication.Service.Domain.UseCases;

public interface ILoginUseCase
{
    Task<UserModel> Execute(LoginModel request);
}