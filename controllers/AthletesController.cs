using Microsoft.AspNetCore.Mvc;
using fitness_tracker.Services;
using fitness_tracker.DTOs;
using System.Security.Claims;
using fitness_tracker.models;
using Microsoft.AspNetCore.Authorization;
namespace fitness_tracker.controllers
{
    [ApiController]
    [Route("api/athletes")]
    public class AthletesController : ControllerBase
    {
        private readonly IAthleteService _athleteService;

        public AthletesController(IAthleteService athleteService)
        {
            _athleteService = athleteService;
        }

        [Authorize(Roles = "Coach,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAthletes()
        {
            var athletes = await _athleteService.GetAllAthletesAsync();

            var result = athletes.Select(a => new AthleteResponseDto
            {
                Id = a.Id,
                FullName = a.FullName,
                Email = a.Email,
                Role = a.Role
            });

            return Ok(result);
        }

        [Authorize(Roles = "Athlete,Coach,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAthleteById(int id)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            if (roleClaim == "Athlete" && userId != id)
                return Forbid();

            var athlete = await _athleteService.GetAthleteByIdAsync(id);

            if (athlete == null)
                return NotFound();

            var result = new AthleteResponseDto
            {
                Id = athlete.Id,
                FullName = athlete.FullName,
                Email = athlete.Email,
                Role = athlete.Role
            };

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAthlete([FromBody] CreateAthleteDto dto)
        {
            var athlete = new Athlete
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            };

            var createdAthlete = await _athleteService.CreateAthleteAsync(athlete);

            var result = new AthleteResponseDto
            {
                Id = createdAthlete.Id,
                FullName = createdAthlete.FullName,
                Email = createdAthlete.Email,
                Role = createdAthlete.Role
            };

            return CreatedAtAction(nameof(GetAthleteById), new { id = createdAthlete.Id }, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAthlete(int id)
        {
            var deleted = await _athleteService.DeleteAthleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [Authorize(Roles = "Athlete,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAthlete(int id, [FromBody] UpdateAthleteDto dto)
        {
            var athlete = new Athlete
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            };

            var updatedAthlete = await _athleteService.UpdateAthleteAsync(id, athlete);

            if (updatedAthlete == null)
                return NotFound();

            var result = new AthleteResponseDto
            {
                Id = updatedAthlete.Id,
                FullName = updatedAthlete.FullName,
                Email = updatedAthlete.Email,
                Role = updatedAthlete.Role
            };

            return Ok(result);
        }
    }
}