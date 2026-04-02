using Microsoft.AspNetCore.Mvc;
using fitness_tracker.models;
using fitness_tracker.Services;
using fitness_tracker.DTOs;
using fitness_tracker.models;
using Microsoft.AspNetCore.Authorization;

namespace fitness_tracker.controllers
{
    [ApiController]
    [Route("api/wods")]
    public class WodsController : ControllerBase
    {
        private readonly IWodService _wodService;
        public WodsController(IWodService wodService)
        {
            _wodService = wodService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllWods()
        {
            var wods = await _wodService.GetAllWodsAsync();

            var result = wods.Select(w => new WodResponseDto
            {
                Id = w.Id,
                Title = w.Title,
                Description = w.Description,
                Category = w.Category,
                Difficulty = w.Difficulty,
                CreatedAt = w.CreatedAt
            });

            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWodById(int id)
        {
            var wod = await _wodService.GetWodByIdAsync(id);

            if (wod == null)
                return NotFound();

            var result = new WodResponseDto
            {
                Id = wod.Id,
                Title = wod.Title,
                Description = wod.Description,
                Category = wod.Category,
                Difficulty = wod.Difficulty,
                CreatedAt = wod.CreatedAt
            };

            return Ok(result);
        }

        [Authorize(Roles = "Coach")]
        [HttpPost]
        public async Task<IActionResult> CreateWod([FromBody] CreateWodDto dto)
        {
            var wod = new Wod
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                Difficulty = dto.Difficulty,
                CoachId = dto.CoachId
            };

            var createdWod = await _wodService.CreateWodAsync(wod);

            var result = new WodResponseDto
            {
                Id = createdWod.Id,
                Title = createdWod.Title,
                Description = createdWod.Description,
                Category = createdWod.Category,
                Difficulty = createdWod.Difficulty,
                CreatedAt = createdWod.CreatedAt
            };

            return CreatedAtAction(nameof(GetWodById), new { id = createdWod.Id }, result);
        }

        [Authorize(Roles = "Coach")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWod(int id, [FromBody] UpdateWodDto dto)
        {
            var wod = new Wod
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                Difficulty = dto.Difficulty
            };

            var updatedWod = await _wodService.UpdateWodAsync(id, wod);

            if (updatedWod == null)
                return NotFound();

            var result = new WodResponseDto
            {
                Id = updatedWod.Id,
                Title = updatedWod.Title,
                Description = updatedWod.Description,
                Category = updatedWod.Category,
                Difficulty = updatedWod.Difficulty,
                CreatedAt = updatedWod.CreatedAt
            };

            return Ok(result);
        }
        
    }
}