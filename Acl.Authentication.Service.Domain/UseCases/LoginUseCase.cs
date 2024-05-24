using Acl.Authentication.Service.Domain.Models;

namespace Acl.Authentication.Service.Domain.UseCases;

public sealed class LoginUseCase : ILoginUseCase
{
    public Task<UserModel> Execute(LoginModel request)
    {
        throw new NotImplementedException();
    }
}