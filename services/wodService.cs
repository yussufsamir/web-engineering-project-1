using fitness_tracker.models;
using fitness_tracker.Data;
using Microsoft.EntityFrameworkCore;

namespace fitness_tracker.Services
{
    public class WodService : IWodService
    {
        private readonly AppDbContext _context;

        public WodService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Wod>> GetAllWodsAsync()
        {
            return await _context.Wods
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Wod?> GetWodByIdAsync(int id)
        {
            return await _context.Wods
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Wod> CreateWodAsync(Wod newWod)
        {
            newWod.CreatedAt = DateTime.Now;
            _context.Wods.Add(newWod);
            await _context.SaveChangesAsync();
            return newWod;
        }
    }
}