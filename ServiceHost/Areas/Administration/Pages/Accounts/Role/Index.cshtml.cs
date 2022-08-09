using System.Collections.Generic;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{

    public class IndexModel : PageModel
    {
        [TempData] 
        public string message { get; set; }
        
        public List<RoleViewModel> Roles;

        private readonly IRoleApplication _roleApplication;
        
        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
            Roles = _roleApplication.GetRoles();
        }
         
        public IActionResult OnGetCreate()
        {
            var command = new CreateRole() { };
            return Partial("./Create", command);
        }


        public JsonResult OnPostCreate(CreateRole createRole)
        {
            var result = _roleApplication.Create(createRole);
            return new JsonResult(result);
        }


        public IActionResult OnGetEdit(long id)
        {
            var role = _roleApplication.GetDetails(id);
            return Partial("./Edit", role);
        }


        public JsonResult OnPostEdit(EditRole editRole)
        {
            var result = _roleApplication.Edit(editRole);
            return new JsonResult(result);
        }
        
    }
}
