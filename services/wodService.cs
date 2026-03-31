using fitness_tracker.models;
using fitness_tracker.Data;
namespace fitness_tracker.Services
{
    public class WodService: IWodService
    {
        private readonly AppDbContext _context;
        public WodService(AppDbContext context)
        {
           _context = context;
        }
        public List<Wod> GetAllWods()
        {
            return _context.Wods.ToList();
        }
        public Wod GetWodById(int id)
        {
            return _context.Wods.FirstOrDefault(w => w.Id == id);
        }
        public Wod CreateWod(Wod newWod)
        {
            newWod.CreatedAt = DateTime.Now;
            _context.Wods.Add(newWod);
            _context.SaveChanges();
            return newWod;
        }
    }
}