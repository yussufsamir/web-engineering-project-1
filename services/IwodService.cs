using fitness_tracker.models;

namespace fitness_tracker.Services
{
    public interface IWodService
    {
        List<Wod> GetAllWods();
        Wod GetWodById(int id);
        Wod CreateWod(Wod newWod);
    }
}