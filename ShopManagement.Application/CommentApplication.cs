using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application
{
   public class CommentApplication:ICommentApplication
   {
       private readonly ICommentRepository _commentRepository;

       public CommentApplication(ICommentRepository commentRepository)
       {
           _commentRepository = commentRepository;
       }

       public OperationResult Add(AddComment command)
       {
           var operation = new OperationResult();

           var comment = new Comment(command.Name,command.Email,command.Message,command.ProductId);

           _commentRepository.Create(comment);

           _commentRepository.SaveChanges();

             return operation.IsSuccess();
       }

        public OperationResult Confirm(long id)
        {
              var operation = new OperationResult();


          var comment= _commentRepository.GetBy(id);

          if (comment == null) return operation.Failed(ApplicationMessages.RecordNotFound);

          comment.Confirm();

           _commentRepository.SaveChanges();

             return operation.IsSuccess();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();


            var comment = _commentRepository.GetBy(id);

            if (comment == null) return operation.Failed(ApplicationMessages.RecordNotFound);

            comment.Cancel();

            _commentRepository.SaveChanges();

            return operation.IsSuccess();
        }

        public List<CommentViewModel> Search(CommentSearchModel command)
        {
            return _commentRepository.Search(command);
        }
    }
}
