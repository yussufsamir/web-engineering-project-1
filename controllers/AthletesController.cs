using Microsoft.AspNetCore.Mvc;
using fitness_tracker.Services;
using fitness_tracker.DTOs;
using fitness_tracker.models;
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

        [HttpGet]
        public IActionResult GetAllAthletes()
        {
            var athletes = _athleteService.GetAllAthletes();
            var result = athletes.Select(a => new AthleteResponseDto
            {
                Id = a.Id,
                FullName = a.FullName,
                Email = a.Email,
                Role = a.Role
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetAthleteById(int id)
        {
            var athlete = _athleteService.GetAthleteById(id);

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
        public IActionResult CreateAthlete([FromBody] CreateAthleteDto dto)
        {
            var newAthlete = new Athlete
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            };

            var createdAthlete = _athleteService.CreateAthlete(newAthlete);

            var result = new AthleteResponseDto
            {
                Id = createdAthlete.Id,
                FullName = createdAthlete.FullName,
                Email = createdAthlete.Email,
                Role = createdAthlete.Role
            };

            return CreatedAtAction(nameof(GetAthleteById), new { id = result.Id }, result);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAthlete(int id)
        {
            var success = _athleteService.DeleteAthlete(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAthlete(int id, [FromBody] UpdateAthleteDto dto)
        {
            var updatedAthlete = new Athlete
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            };

            var athlete = _athleteService.UpdateAthlete(id, updatedAthlete);

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
    }
}