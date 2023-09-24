using WebApiTravel.Models;

namespace WebApiTravel.Repository.IRepository
{
    public interface ITravelNumberRepository : IRepository<TravelNumber>
    {
        Task<TravelNumber> UpdateAsync(TravelNumber entity);
    }
}
