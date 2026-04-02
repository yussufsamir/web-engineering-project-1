[Authorize(Roles = "Coach,Admin")]
[HttpGet("coach")]
public IActionResult GetCoachDashboard()
{
    var totalAthletes = _context.Athletes.AsNoTracking().Count();
    var totalWods = _context.Wods.AsNoTracking().Count();
    var totalAssignments = _context.WorkoutAssignments.AsNoTracking().Count();
    var pendingAssignments = _context.WorkoutAssignments.AsNoTracking().Count(a => a.Status == "Assigned");
    var completedAssignments = _context.WorkoutAssignments.AsNoTracking().Count(a => a.Status == "Completed");

    var today = DateTime.Today;
    var tomorrow = today.AddDays(1);

    var assignmentsDueToday = _context.WorkoutAssignments
        .AsNoTracking()
        .Count(a => a.DueDate >= today && a.DueDate < tomorrow);

    var recentAssignments = _context.WorkoutAssignments
        .AsNoTracking()
        .Include(a => a.Athlete)
        .Include(a => a.Wod)
        .OrderByDescending(a => a.AssignedDate)
        .Take(5)
        .Select(a => new AssignmentResponseDto
        {
            Id = a.Id,
            AthleteId = a.AthleteId,
            AthleteName = a.Athlete.FullName,
            WodId = a.WodId,
            WodTitle = a.Wod.Title,
            AssignedDate = a.AssignedDate,
            DueDate = a.DueDate,
            Status = a.Status,
            Notes = a.Notes
        })
        .ToList();

    var result = new CoachDashboardDto
    {
        TotalAthletes = totalAthletes,
        TotalWods = totalWods,
        TotalAssignments = totalAssignments,
        PendingAssignments = pendingAssignments,
        CompletedAssignments = completedAssignments,
        AssignmentsDueToday = assignmentsDueToday,
        RecentAssignments = recentAssignments
    };

    return Ok(result);
}