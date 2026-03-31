using fitness_tracker.models;

namespace fitness_tracker.Services
{
    public interface IAthleteService
    {
        List<Athlete> GetAllAthletes();
        Athlete GetAthleteById(int id);
        Athlete CreateAthlete(Athlete newAthlete);
        bool DeleteAthlete(int id);
        Athlete UpdateAthlete(int id, Athlete updatedAthlete);
    }
}