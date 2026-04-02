using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using fitness_tracker.Data;
using fitness_tracker.DTOs;

namespace fitness_tracker.controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class CoachDashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoachDashboardController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Coach,Admin")]
        [HttpGet("coach")]
        public IActionResult GetCoachDashboard()
        {
            // Basic counts
            var totalAthletes = _context.Athletes
                .AsNoTracking()
                .Count();

            var totalWods = _context.Wods
                .AsNoTracking()
                .Count();

            var totalAssignments = _context.WorkoutAssignments
                .AsNoTracking()
                .Count();

            // Recent assignments (last 5)
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
                RecentAssignments = recentAssignments
            };

            return Ok(result);
        }
    }
}