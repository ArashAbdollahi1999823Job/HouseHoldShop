using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _accountContext;
        public AccountRepository(AccountContext context) : base(context)
        {
            _accountContext = context;
        }

        public List<AccountViewModel> Search(AccountSearchModel command)
        {

            var query = 
                _accountContext
                .Accounts
                .Include(x=>x.Role)
                .Select(x => new AccountViewModel()
                {
                    Id = x.Id,
                    Mobile = x.Mobile,
                    Username = x.Username,
                    ProfilePhoto = x.ProfilePhoto,
                    Fullname = x.Fullname,
                    Role = x.Role.Name,
                    RoleId = x.Role.Id,
                    CreationDate = x.CreationDate.ToFarsi(),
                });

            if(!string.IsNullOrWhiteSpace(command.Fullname))query=query.Where(x => x.Fullname.Contains(command.Fullname));
            if(!string.IsNullOrWhiteSpace(command.Mobile))query=query.Where(x => x.Mobile.Contains(command.Mobile));
            if(!string.IsNullOrWhiteSpace(command.Username)) query = query.Where(x => x.Username.Contains(command.Username));
            if(command.RoleId>0) query = query.Where(x => x.RoleId== command.RoleId);

           return query.OrderByDescending(x=>x.Id).ToList();

        }

        public EditAccount GetDetails(long id)
        {
            return 
                _accountContext
                    .Accounts
                    .Select(x => new EditAccount()
                    {
                        Fullname = x.Fullname,
                        Id = x.Id,
                        Mobile = x.Mobile,
                        RoleId = x.RoleId,
                        Username = x.Username,
                    })
                    .FirstOrDefault(x => x.Id == id) ?? throw new InvalidOperationException();
        }

        public Account GetByUserName(string userName)
        {
            return _accountContext.Accounts.FirstOrDefault(predicate: x => x.Username == userName);
        }
    }
}
