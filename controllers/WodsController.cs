using Microsoft.AspNetCore.Mvc;
using fitness_tracker.models;
using fitness_tracker.Services;
using fitness_tracker.DTOs;
using fitness_tracker.models;

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

        [HttpGet]
        public IActionResult GetAllWods()
        {
            var wods = _wodService.GetAllWods();
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

        [HttpGet("{id}")]
        public IActionResult GetWodById(int id)
        {
            var wod = _wodService.GetWodById(id);

            if (wod == null)
                return NotFound();
            
            var res= new WodResponseDto
            {
                Id = wod.Id,
                Title = wod.Title,
                Description = wod.Description,
                Category = wod.Category,
                Difficulty = wod.Difficulty,
                CreatedAt = wod.CreatedAt
            };
            return Ok(res);
        }

        [HttpPost]
        public IActionResult CreateWod([FromBody] CreateWodDto dto)
        {
            var newWod = new Wod
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                Difficulty = dto.Difficulty,
                CoachId = dto.CoachId
            };

            var createdWod = _wodService.CreateWod(newWod);


            return CreatedAtAction(nameof(GetWodById), new { id = createdWod.Id }, createdWod);
        }
    }
}