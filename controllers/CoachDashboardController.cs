using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using fitness_tracker.Data;
using fitness_tracker.DTOs;

namespace fitness_tracker.controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Coach")]
        [HttpGet("coach")]
        public IActionResult GetCoachDashboard()
        {
            var totalAthletes = _context.Athletes.Count();
            var totalWods = _context.Wods.Count();
            var totalAssignments = _context.WorkoutAssignments.Count();

            var recentAssignments = _context.WorkoutAssignments
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
                RecentAssignments = recentAssignments
            };

            return Ok(result);
        }
    }
}