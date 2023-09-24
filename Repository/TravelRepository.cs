using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiTravel.Data;
using WebApiTravel.Models;
using WebApiTravel.Repository.IRepository;

namespace WebApiTravel.Repository
{
    public class TravelRepository : Repository<Travel>,ITravelRepository
    {
        private readonly ApplicationDbContext _context;
        public TravelRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }
        
        public async Task<Travel> UpdateAsync(Travel entity)
        {
            entity.UpdateDate = DateTime.Now;
            _context.Travels.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
