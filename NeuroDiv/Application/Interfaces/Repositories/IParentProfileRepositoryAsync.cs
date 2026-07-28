using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IParentProfileRepositoryAsync : IGenericRepositoryAsync<ParentProfile>
    {
        Task<ParentProfile?> GetByIdAsync(Guid id);
        Task<ChildParent> AddChildParentAsync(ChildParent childParent);
    }
}
