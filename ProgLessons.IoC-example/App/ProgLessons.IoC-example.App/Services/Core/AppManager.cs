using System;
using Microsoft.Extensions.DependencyInjection;
using ProgLessons.IoC_example.App.Services.IoC;
using ProgLessons.IoC_example.App.Services.Auth;
using ProgLessons.IoC_example.App.Services.UI;

namespace ProgLessons.IoC_example.App.Services.Core
{
    public enum ServiceExecutionResult { Undefined, Success, Failure };
    public enum ServiceExecutionStatus { Running, Completed };

    public class AppManager
    {
        IServiceProvider svcPvd;
        IUserInterfaceService uiService;

        public AppManager(Type TUserInterfaceHandler)
        {
            var contConfig = new ContainerConfigurator();

            contConfig.DynamicServices.Add(typeof(IUserInterfaceService), TUserInterfaceHandler);
            svcPvd = contConfig.ConfigureServices();
        }

        public async void Run()
        {
            this.uiService = svcPvd.GetService<IUserInterfaceService>();
            this.uiService.OutputInfo("--* IoC Example App *--", InfoCategory.Title);

            var authService = svcPvd.GetService<IAuthenticationService>();

            var t = await authService.SignInTask();

            var msg = "";

            if (t == ServiceExecutionResult.Failure)
                msg = "It's not possible to proceed.";
            else
                msg = "You're in.";

            uiService.OutputInfo(msg, InfoCategory.Response);
            if (uiService.RequestAppStop != null) Stop(uiService.RequestAppStop);
        }

        public void Stop(Action terminateAct) => terminateAct.Invoke();
    }
}