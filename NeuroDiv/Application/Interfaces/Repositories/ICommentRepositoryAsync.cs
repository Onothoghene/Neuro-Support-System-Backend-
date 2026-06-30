using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ICommentRepositoryAsync : IGenericRepositoryAsync<Comments>
    {
        IQueryable<Comments> GetFoodComments(string foodId);
        Task<Comments> GetCommentById(string id);
        IQueryable<Comments> GetUserComments(string userId);
        IQueryable<Comments> GetUserFoodComments(string userId, string foodId);
    }
}
