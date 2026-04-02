using fitness_tracker.models;

namespace fitness_tracker.Services
{
    public interface IAthleteService
    {
        Task<List<Athlete>> GetAllAthletesAsync();
        Task<Athlete?> GetAthleteByIdAsync(int id);
        Task<Athlete> CreateAthleteAsync(Athlete newAthlete);
        Task<Athlete?> UpdateAthleteAsync(int id, Athlete updatedAthlete);
        Task<bool> DeleteAthleteAsync(int id);
    }
}