using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {

        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var inventory = new Inventory(command.ProductId, command.UnitPrice);
                _inventoryRepository.Create(inventory);
                _inventoryRepository.SaveChanges();
                return operation.IsSuccess();
            }
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetById(command.ProductId);
            if (inventory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else if (_inventoryRepository
               .Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                inventory.Edit(command.ProductId, command.UnitPrice);
                _inventoryRepository.SaveChanges();

                return operation.IsSuccess();
            }
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.InventoryId);

            if (inventory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                const long operatorId = 1;
                inventory.Increase(command.Count, operatorId, command.Description);
                _inventoryRepository.SaveChanges();
                return operation.IsSuccess();
            }
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.InventoryId);

            if (inventory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                const long operatorId = 1;
                inventory.Reduce(command.Count, operatorId, command.Description, 0);
                _inventoryRepository.SaveChanges();
                return operation.IsSuccess();
            }
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation = new OperationResult();
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.InventoryId);

                if (inventory == null)
                {
                    return operation.Failed(ApplicationMessages.RecordNotFound);
                }
                else
                {
                    const long operatorId = 1;
                    inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);
                }
            }
            _inventoryRepository.SaveChanges();
            return operation.IsSuccess();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            return _inventoryRepository.GetOperationLog(inventoryId);
        }
    }
}
