using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication:IRoleApplication
    {

        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }



        public OperationResult Create(CreateRole createRole)
        {
            var operationResult = new OperationResult();

            if (_roleRepository.Exists(x => x.Name == createRole.Name))
            {
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var role = new Role(createRole.Name);
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();

            return operationResult.IsSuccess();

        }

        public OperationResult Edit(EditRole editRole)
        {
            var operationResult = new OperationResult();

            var role = _roleRepository.GetBy(editRole.Id);


            if (role == null) return operationResult.Failed(ApplicationMessages.RecordNotFound);
            
            if (_roleRepository.Exists(x => x.Name == editRole.Name && x.Id !=editRole.Id ))
            {
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            
            role.Edit(editRole.Name);
            _roleRepository.SaveChanges();

            return operationResult.IsSuccess();
        }

        public List<RoleViewModel> GetRoles()
        {
            return _roleRepository.GetRoles();
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }
    }
}
