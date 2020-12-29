namespace ProgLessons.IoC_example.App.Services.Auth
{
    public class UserAccountsManager
    {
        UserAccountsRegister userAccounts;

        public UserAccountsManager(UserAccountsRegister userAccounts)
        {
            this.userAccounts = userAccounts;
            LoadAccounts();
        }

        void LoadAccounts()
        {
            userAccounts.Add(new UserAccount { UserName = "user1", Passwd = "123456" });
            userAccounts.Add(new UserAccount { UserName = "user2", Passwd = "123456" });
            userAccounts.Add(new UserAccount { UserName = "cadu", Passwd = "tech" });
        }

        public UserAccount Find(string userName) => userAccounts.Find(u => u.UserName == userName);
    }
}