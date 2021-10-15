using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EfCore;

namespace DiscountManagement.infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository:RepositoryBase<long,CustomerDiscount>,ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopcontext;

        public CustomerDiscountRepository(DiscountContext context, ShopContext shopcontext) :base(context)
        {
            _context = context;
            _shopcontext = shopcontext;
        }


        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount()
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToString(),
                ProductId = x.ProductId,
                Reason = x.Reason,
                StartDate = x.StartDate.ToString(),

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopcontext.Products.Select(x => new {x.Id, x.Name}).ToList();


            var query =
                _context
                    .CustomerDiscounts
                    .Select(x => new CustomerDiscountViewModel()
                    {
                        Id = x.Id,
                        DiscountRate = x.DiscountRate,
                        EndDate = x.EndDate.ToFarsi(),
                        StartDate = x.StartDate.ToFarsi(),
                        
                        EndDateGr = x.EndDate,
                        StartDateGr = x.StartDate,
                        

                        ProductId = x.ProductId,
                        Reason = x.Reason,
                    });


            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }


            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query
                    .Where(x => x.StartDateGr < searchModel.StartDate.ToGeorgianDateTime());
            }


            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                query = query
                .Where(x => x.StartDateGr < searchModel.EndDate.ToGeorgianDateTime());
            }

            var discounts =
                query.OrderByDescending(x => x.Id).ToList();


            discounts
                .ForEach(
                    item => item
                        .Product = products
                        .FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return discounts;


        }
    }
}
