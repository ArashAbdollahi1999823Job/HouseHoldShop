using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;

        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }


        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    Message = x.Message,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    Name = x.Name,
                    CommentDate = x.CreationDate.ToFarsi(),
                    Website = x.Website,


                    OwnerType = x.Type,
                    /*OwnerRecordId = x.OwnerRecordId,
                    OwnerRecordName = x.*/

                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query.Where(x => x.Name.Contains(searchModel.Email));

            return query.OrderByDescending(x => x.Id).ToList();


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
