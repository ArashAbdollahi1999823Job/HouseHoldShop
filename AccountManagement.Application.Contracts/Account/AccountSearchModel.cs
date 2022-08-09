namespace AccountManagement.Application.Contracts.Account;

public class AccountSearchModel
{
    public string Fullname { get; private set; }
    public string Username { get; private set; }
    public string Mobile { get; private set; }
    public string Role { get; private set; }

    public long RoleId { get; set; }

}