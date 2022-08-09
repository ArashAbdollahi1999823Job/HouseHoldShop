using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        #region CtorAndInjection
        private readonly IAccountApplication _accountApplication;

        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }
        #endregion

        [TempData]
        public string Message { get; set; }


        public void OnGet()
        {
        }

        #region Login
        public IActionResult OnPostLogin(Login login)
        {
            var result=_accountApplication.Login(login);

            if (result.IsSuccedded) return RedirectToPage("index");


            Message = result.Message;
            return Page();
        }
        #endregion

        #region Logout
        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("index");
        }
        #endregion
    }
}
