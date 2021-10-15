using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository:RepositoryBase<long,Comment>,ICommentRepository
    {
        private readonly ShopContext _context;

        public CommentRepository(ShopContext context):base(context)
        {
            _context = context;
        }


        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
                .Include(x => x.Product)
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Email = x.Email,
                    Message = x.Message,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    Name = x.Name,
                    ProductName = x.Product.Name,
                    CommentDate = x.CreationDate.ToFarsi(),
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query.Where(x => x.Name.Contains(searchModel.Email));

            return query.OrderByDescending(x=>x.Id).ToList();


            /*   var query = _context.Comments
                   .Include(x => x.Product)
                   .Where(x => x.Name.Contains(searchModel.Name) || x.Email.Contains(searchModel.Email))
                   .Select(x => new CommentViewModel
                   {
                       Id = x.Id,
                       ProductId = x.ProductId,
                       Email = x.Email,
                       Message = x.Message,
                       IsCanceled = x.IsCanceled,
                       IsConfirmed = x.IsConfirmed,
                       Name = x.Name,
                       ProductName = x.Product.Name,
                       CommentDate = x.CreationDate.ToFarsi(),
                   }).ToList();*/
        }

    }
}
