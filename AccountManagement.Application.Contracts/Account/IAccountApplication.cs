using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Create(CreateAccount command);
        OperationResult Edit(EditAccount command);
        EditAccount GetDetails(long id);
        OperationResult ChangePassword(ChangePasswordModel command);
        List<AccountViewModel> Search(AccountSearchModel command);
        OperationResult Login(Login command);
        void Logout();
    }
}
