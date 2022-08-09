﻿using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {

        private readonly AccountContext _accountContext;


        public RoleRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }




        public List<RoleViewModel> GetRoles()
        {
            return
                _accountContext
                    .Roles
                    .Select(x => new RoleViewModel  
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CreationDate=x.CreationDate.ToFarsi(),
                    })
                    .ToList();
        }

        public EditRole GetDetails(long id)
        {
            return 
                _accountContext
                .Roles
                .Select(x => new EditRole
                {
                    Id = x.Id,
                    Name = x.Name
                })
                  .FirstOrDefault(x => x.Id == id);
        }
    }
}
