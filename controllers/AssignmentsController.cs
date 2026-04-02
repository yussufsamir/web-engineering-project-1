using Microsoft.AspNetCore.Mvc;
using fitness_tracker.models;
using fitness_tracker.Services;
using fitness_tracker.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace fitness_tracker.controllers
{
    [ApiController]
    [Route("api/assignments")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [Authorize(Roles = "Athlete,Coach")]
        [HttpGet("athlete/{athleteId}")]
        public async Task<IActionResult> GetAssignmentsByAthleteId(int athleteId)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            if (roleClaim == "Athlete" && userId != athleteId)
                return Forbid();

            var assignments = await _assignmentService.GetAssignmentsByAthleteIdAsync(athleteId);

            if (!assignments.Any())
                return NotFound();

            var result = assignments.Select(a => new AssignmentResponseDto
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
            });

            return Ok(result);
        }

        [Authorize(Roles = "Coach")]
        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentDto dto)
        {
            var assignment = new WorkoutAssignment
            {
                AthleteId = dto.AthleteId,
                WodId = dto.WodId,
                DueDate = dto.DueDate,
                Status = dto.Status,
                Notes = dto.Notes
            };

            var resultFromService = await _assignmentService.CreateAssignmentAsync(assignment);

            if (resultFromService.Assignment == null)
                return BadRequest(new { message = resultFromService.ErrorMessage });

            var createdAssignment = resultFromService.Assignment;

            var result = new AssignmentResponseDto
            {
                Id = createdAssignment.Id,
                AthleteId = createdAssignment.AthleteId,
                AthleteName = createdAssignment.Athlete.FullName,
                WodId = createdAssignment.WodId,
                WodTitle = createdAssignment.Wod.Title,
                AssignedDate = createdAssignment.AssignedDate,
                DueDate = createdAssignment.DueDate,
                Status = createdAssignment.Status,
                Notes = createdAssignment.Notes
            };

            return CreatedAtAction(nameof(GetAssignmentsByAthleteId), new { athleteId = createdAssignment.AthleteId }, result);
        }
    }
}