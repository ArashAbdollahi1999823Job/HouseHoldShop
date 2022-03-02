using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace Discount.Management.Application
{
    public class CustomerDiscountApplication:ICustomerDiscountApplication
    {
        public readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
           var operation=new OperationResult();

           if (_customerDiscountRepository
               .Exists(x => x.ProductId == command.ProductId
                            && x.DiscountRate == command.DiscountRate))
           {
               return operation.Failed(ApplicationMessages.DuplicatedRecord);
           }else 
           {
                var customerDiscount=new CustomerDiscount(command.ProductId
                    ,command.DiscountRate, command.StartDate.ToGeorgianDateTime()
                    ,command.EndDate.ToGeorgianDateTime(),command.Reason);


                _customerDiscountRepository.Create(customerDiscount);
                _customerDiscountRepository.SaveChanges();
                return operation.IsSuccess();
           }


        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerDiscount = _customerDiscountRepository.GetBy(command.Id);

            if (customerDiscount == null)
            {
              return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else if (_customerDiscountRepository.Exists(x=>
                x.ProductId == command.ProductId
                && x.DiscountRate == command.DiscountRate
                && x.Id != command.Id))
            {
               return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                 customerDiscount.Edit(command.ProductId
                    , command.DiscountRate, command.StartDate.ToGeorgianDateTime()
                    , command.EndDate.ToGeorgianDateTime(), command.Reason);

                 _customerDiscountRepository.SaveChanges();
                 return operation.IsSuccess();
            }

        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}
