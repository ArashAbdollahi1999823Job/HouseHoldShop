using System.Collections.Generic;
using _0_Framework.Domain;
using InventoryManagement.Application.Contract.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        EditInventory GetDetails(long id);
        Inventory GetById(long id);
        List<InventoryViewModel> Search(InventorySearchModel command);

        List<InventoryOperationViewModel> GetOperationLog(long InventoryId);

    }
}
