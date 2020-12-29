using System.Collections.Generic;

namespace ProgLessons.IoC_example.App.Services.Auth
{
    public class UserAccount
    {
        public string UserName { get; set; }
        public string Passwd { get; set; }
    }

    public class UserAccountsRegister : List<UserAccount> { }
}
