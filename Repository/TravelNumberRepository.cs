using WebApiTravel.Data;
using WebApiTravel.Models;
using WebApiTravel.Repository.IRepository;

namespace WebApiTravel.Repository
{
    public class TravelNumberRepository : Repository<TravelNumber>, ITravelNumberRepository
    {
        private readonly ApplicationDbContext _context;
        public TravelNumberRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TravelNumber> UpdateAsync(TravelNumber entity)
        {
            entity.UpdateDate = DateTime.Now;
            _context.TravelNumbers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
