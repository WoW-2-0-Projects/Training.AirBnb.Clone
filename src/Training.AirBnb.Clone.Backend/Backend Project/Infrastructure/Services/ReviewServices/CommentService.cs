using Backend_Project.Application.Foundations.ReviewServices;
using Backend_Project.Application.Review.Settings;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ReviewServices
{
    public class CommentService : ICommentService
    {
        private readonly IDataContext _appDataContext;
        private readonly ReviewSettings _commentSettings;

        public CommentService(IDataContext appDataContext, IOptions<ReviewSettings> commentSettings)
        {
            _appDataContext = appDataContext;
            _commentSettings = commentSettings.Value;
        }

        public async ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (!IsValidCommentMessage(comment.CommentMessage))
                throw new EntityValidationException<Comment>("Invalid comment!");

            await _appDataContext.Comments.AddAsync(comment, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return comment;
        }

        public ValueTask<ICollection<Comment>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var comments = GetUndeletedComments().
                Where(comment => ids.Contains(comment.Id));
            return new ValueTask<ICollection<Comment>>(comments.ToList());
        }

        public ValueTask<Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return new ValueTask<Comment>(GetUndeletedComments().
                FirstOrDefault(comment => comment.Id == id) ??
                throw new EntityNotFoundException<Comment>("Comment not found!"));
        }

        public IQueryable<Comment> Get(Expression<Func<Comment, bool>> predicate)
        {
            return GetUndeletedComments().Where(predicate.Compile()).AsQueryable();
        }

        public async ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var updatedComment = await GetByIdAsync(comment.Id, cancellationToken);

            if (!IsValidCommentMessage(comment.CommentMessage))
                throw new EntityValidationException<Comment>("Invalid comment!");

            updatedComment.CommentMessage = comment.CommentMessage;

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return updatedComment;
        }

        public async ValueTask<Comment> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var deletedComment = await GetByIdAsync(id,cancellationToken);
            
            await _appDataContext.Comments.RemoveAsync(deletedComment, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return deletedComment;
        }

        public async ValueTask<Comment> DeleteAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
            => await DeleteAsync(comment.Id, saveChanges, cancellationToken);
       
        private bool IsValidCommentMessage(string commentMessage)
        {
            if (!string.IsNullOrWhiteSpace(commentMessage) && commentMessage.Length <= _commentSettings.MaxCommentLength)
                return true;
            else return false;
        }

        private IQueryable<Comment> GetUndeletedComments() => _appDataContext.Comments.
            Where(comment => !comment.IsDeleted).AsQueryable();
    }
}