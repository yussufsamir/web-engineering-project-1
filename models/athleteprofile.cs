namespace fitness_tracker.models
{
    public class AthleteProfile
    {
        public int Id { get; set; }
        public int AthleteId { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Goal { get; set; }
        public string ExperienceLevel { get; set; }
    }
}