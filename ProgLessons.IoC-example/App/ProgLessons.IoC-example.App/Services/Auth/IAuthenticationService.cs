using System.Threading.Tasks;
using ProgLessons.IoC_example.App.Services.Core;

namespace ProgLessons.IoC_example.App.Services.Auth
{
    public enum AuthStatus { Undefined = -1, AccountValidated = 0, UserNotFound = 1, IncorrectPasswd = 2 };

    public interface IAuthenticationService
    {
        Task<ServiceExecutionResult> SignInTask();
    }
}
