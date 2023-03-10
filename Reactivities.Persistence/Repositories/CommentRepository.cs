using Infrastructure;
using Infrastructure.Repositories;
using Reactivities.Domain.Interfaces;
using Reactivities.Domain.Models;

namespace Reactivities.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context)
        {
        }
    }
}
