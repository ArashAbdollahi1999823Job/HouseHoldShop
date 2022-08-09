using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Fullname { get;  set; }

        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Username { get;  set; }

        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Password { get;  set; }

        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Mobile { get;  set; }
        public IFormFile ProfilePhoto { get;  set; }

        public long RoleId { get;  set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
