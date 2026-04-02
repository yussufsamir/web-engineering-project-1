using fitness_tracker.models;

namespace fitness_tracker.Services
{
    public interface IWodService
    {
        Task<List<Wod>> GetAllWodsAsync();
        Task<Wod?> GetWodByIdAsync(int id);
        Task<Wod> CreateWodAsync(Wod newWod);
        Task<Wod?> UpdateWodAsync(int id, Wod updatedWod);
    }
}