using fitness_tracker.models;
using fitness_tracker.Data;
namespace fitness_tracker.Services
{
    public class AthleteService : IAthleteService
    {
        private readonly AppDbContext _context;
        public AthleteService(AppDbContext context)
        {
            _context = context;
        }
        public List<Athlete> GetAllAthletes()
        {
            return _context.Athletes.ToList();
        }

        public Athlete GetAthleteById(int id)
        {
            return _context.Athletes.FirstOrDefault(a => a.Id == id);
        }
        public Athlete CreateAthlete(Athlete newAthlete)
        {
            _context.Athletes.Add(newAthlete);
            _context.SaveChanges();
            return newAthlete;
        }
        public bool DeleteAthlete(int id)
        {
            var athlete = _context.Athletes.FirstOrDefault(a => a.Id == id);

            if (athlete == null)
                return false;

            _context.Athletes.Remove(athlete);
            _context.SaveChanges();

            return true;
        }
        public Athlete UpdateAthlete(int id, Athlete updatedAthlete)
        {
            var athlete = _context.Athletes.FirstOrDefault(a => a.Id == id);

            if (athlete == null)
                return null;

            athlete.FullName = updatedAthlete.FullName;
            athlete.Email = updatedAthlete.Email;
            athlete.Password = updatedAthlete.Password;
            athlete.Role = updatedAthlete.Role;

            _context.SaveChanges();

            return athlete;
        }

    }
}