using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Application.contracts.Comment;

namespace ShopManagement.Domain.CommentAgg
{
    public interface ICommentRepository:IRepository<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
