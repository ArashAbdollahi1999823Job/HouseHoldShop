using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole createRole);

        OperationResult Edit(EditRole editRole);

        List<RoleViewModel> GetRoles();

        EditRole GetDetails(long id);

    }
}
