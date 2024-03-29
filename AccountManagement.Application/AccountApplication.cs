﻿  using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {

        #region CtorAndInjection
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;
        private readonly IAuthHelper _authHelper;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader, IAuthHelper authHelper)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
        }
        #endregion

     


        #region CreateAccount
        public OperationResult Create(CreateAccount command)
        {
            var operation = new OperationResult();

            if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile ==command.Mobile))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var password = _passwordHasher.Hash(command.Password);
            var path = $"ProfilePhoto/{command.Username}";
            var photo = _fileUploader.Upload(command.ProfilePhoto, path);
            var account = new Account(command.Fullname, command.Username, password, command.Mobile, photo,
                command.RoleId);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();

            return operation.IsSuccess();

        }
        #endregion

        #region UpdateAccount
        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Id);
            if (account == null) return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_accountRepository.Exists(x => (x.Username == command.Username || x.Mobile == command.Mobile ) && x.Id !=command.Id ))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var path = $"ProfilePhoto/{command.Username}";
            var photo = _fileUploader.Upload(command.ProfilePhoto, path);

            account.Edit(command.Fullname, command.Username,  command.Mobile, photo, command.RoleId);

            _accountRepository.SaveChanges();

            return operation.IsSuccess();

        }
        #endregion

        #region GetAccountById
        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public OperationResult ChangePassword(ChangePasswordModel command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Id);
            if (account == null) return operation.Failed(ApplicationMessages.RecordNotFound);


            if (command.Password != command.RePassword) operation.Failed(ApplicationMessages.PasswordNotMatch);

            var password = _passwordHasher.Hash(command.Password);



            account.ChangePassword(password);

            _accountRepository.SaveChanges();

            return operation.IsSuccess();

        }
        #endregion

        #region SearchAccount
        public List<AccountViewModel> Search(AccountSearchModel command)
        {
            return _accountRepository.Search(command);
        }
        #endregion

        #region Login
        public OperationResult Login(Login command)
        {
            #region CheckeUser
            var operation = new OperationResult();
            var account = _accountRepository.GetByUserName(command.UserName);
            if(account== null) { operation.Failed(ApplicationMessages.WrongUserPass); }
            (bool Verified,bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);
            if (!result.Verified) { return operation.Failed(ApplicationMessages.WrongUserPass);}
            #endregion


            #region SignIn

            var authViewModel = new AuthViewModel(account.Id,account.RoleId,account.Fullname,account.Username);
            _authHelper.SignIn(authViewModel);
            #endregion




            return operation.IsSuccess();
        }
        #endregion

        #region Logout
        public void Logout()
        {
            _authHelper.SignOut();
        }
        #endregion

    }
}
