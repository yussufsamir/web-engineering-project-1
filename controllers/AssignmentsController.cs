using Microsoft.AspNetCore.Mvc;
using fitness_tracker.models;
using fitness_tracker.Services;
using fitness_tracker.DTOs;

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

        [HttpGet("athlete/{athleteId}")]
        public IActionResult GetAssignmentsByAthleteId(int athleteId)
        {
            var assignments = _assignmentService.GetAssignmentsByAthleteId(athleteId);

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

        [HttpPost]
        public IActionResult CreateAssignment([FromBody] CreateAssignmentDto dto)
        {
        var assignment = new WorkoutAssignment
        {
            AthleteId = dto.AthleteId,
            WodId = dto.WodId,
            DueDate = dto.DueDate,
            Status = dto.Status,
            Notes = dto.Notes
        };

        var resultFromService = _assignmentService.CreateAssignment(assignment);

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