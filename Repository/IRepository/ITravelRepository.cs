using System.Linq.Expressions;
using WebApiTravel.Models;

namespace WebApiTravel.Repository.IRepository
{
    public interface ITravelRepository : IRepository<Travel>
    {
        Task<Travel> UpdateAsync(Travel entity);
    }
}
