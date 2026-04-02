using fitness_tracker.models;
using fitness_tracker.Data;
using Microsoft.EntityFrameworkCore;

namespace fitness_tracker.Services
{
    public class AthleteService : IAthleteService
    {
        private readonly AppDbContext _context;

        public AthleteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Athlete>> GetAllAthletesAsync()
        {
            return await _context.Athletes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Athlete?> GetAthleteByIdAsync(int id)
        {
            return await _context.Athletes
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Athlete> CreateAthleteAsync(Athlete newAthlete)
        {
            _context.Athletes.Add(newAthlete);
            await _context.SaveChangesAsync();
            return newAthlete;
        }

        public async Task<Athlete?> UpdateAthleteAsync(int id, Athlete updatedAthlete)
        {
            var athlete = await _context.Athletes.FirstOrDefaultAsync(a => a.Id == id);

            if (athlete == null)
                return null;

            athlete.FullName = updatedAthlete.FullName;
            athlete.Email = updatedAthlete.Email;
            athlete.Password = updatedAthlete.Password;
            athlete.Role = updatedAthlete.Role;

            await _context.SaveChangesAsync();
            return athlete;
        }

        public async Task<bool> DeleteAthleteAsync(int id)
        {
            var athlete = await _context.Athletes.FirstOrDefaultAsync(a => a.Id == id);

            if (athlete == null)
                return false;

            _context.Athletes.Remove(athlete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}