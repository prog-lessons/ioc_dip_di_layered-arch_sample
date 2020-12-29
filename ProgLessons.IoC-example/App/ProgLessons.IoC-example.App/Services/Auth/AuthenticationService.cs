using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ProgLessons.IoC_example.App.Services.Core;
using ProgLessons.IoC_example.App.Services.UI;

namespace ProgLessons.IoC_example.App.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        UserAccountsManager userAccountsManager;
        IUserInterfaceService uiService;
        ServiceExecutionStatus status = ServiceExecutionStatus.Running;
        ServiceExecutionResult result = ServiceExecutionResult.Undefined;
        int remainingAttempts = 3;

        public AuthenticationService(UserAccountsManager userAccountsManager, IUserInterfaceService userInterfaceService)
        {
            this.userAccountsManager = userAccountsManager;
            uiService = userInterfaceService;

            uiService.BreakLine();
            uiService.OutputInfo("<Authentication>", InfoCategory.SubTitle);
            uiService.BreakLine();
        }

        public async Task<ServiceExecutionResult> SignInTask()
        {
            var loginField = new UIField("Username");
            var passwField = new UIField("Password");
            passwField.HideChars = true;

            var inputFields = new List<UIField>();
            inputFields.Add(loginField);
            inputFields.Add(passwField);

            if (uiService.SingleThreaded)
            {
                while (this.status != ServiceExecutionStatus.Completed)
                    uiService.WaitUserInput(inputFields, new Action(Authenticate));
            }
            else
            {
                uiService.WaitUserInput(inputFields, new Action(Authenticate));
                await MonitorServiceStatus();
            }

            return this.result;
        }

        async Task MonitorServiceStatus()
        {
            while (true)
            {
                if (this.status == ServiceExecutionStatus.Completed) break;
                await Task.Delay(1000);
            }
        }

        public void Authenticate()
        {
            var status_messages = new Dictionary<AuthStatus, string>();

            status_messages.Add(AuthStatus.AccountValidated, "Account validated successfully.");
            status_messages.Add(AuthStatus.UserNotFound, "Username not found.");
            status_messages.Add(AuthStatus.IncorrectPasswd, "Incorrect password.");

            AuthStatus status;

            var userAccount = userAccountsManager.Find(uiService.GetUIField("Username").Value);

            if (userAccount == null)
                status = AuthStatus.UserNotFound;
            else
            if (userAccount.Passwd != uiService.GetUIField("Password").Value)
                status = AuthStatus.IncorrectPasswd;
            else
                status = AuthStatus.AccountValidated;

            if (status == AuthStatus.AccountValidated)
                this.result = ServiceExecutionResult.Success;
            else
            {
                remainingAttempts--;
                if (remainingAttempts == 0) this.result = ServiceExecutionResult.Failure;
            }

            uiService.BreakLine();
            uiService.OutputInfo(status_messages[status], InfoCategory.FixedResponse);

            if (this.result != ServiceExecutionResult.Undefined) this.status = ServiceExecutionStatus.Completed;
        }
    }
}