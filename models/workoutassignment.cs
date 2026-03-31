using fitness_tracker.models;
public class WorkoutAssignment
{
    public int Id { get; set; }

    public int AthleteId { get; set; }
    public Athlete Athlete { get; set; }//many to one relationship

    public int WodId { get; set; }
    public Wod Wod { get; set; } //many to one relationship

    public DateTime AssignedDate { get; set; }
    public DateTime DueDate { get; set; }

    public string Status { get; set; }
    public string Notes { get; set; }
}