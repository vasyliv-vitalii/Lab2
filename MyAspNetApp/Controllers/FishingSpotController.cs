using FishingAndCyclingApp.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FishingAndCyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FishingSpotsController : ControllerBase
    {
        private readonly IRepository<FishingSpot> _fishingSpotRepository;

        public FishingSpotsController(IRepository<FishingSpot> fishingSpotRepository)
        {
            _fishingSpotRepository = fishingSpotRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FishingSpotDto>>> GetFishingSpots()
        {
            var fishingSpots = await _fishingSpotRepository.GetAllAsync();
            var fishingSpotDtos = fishingSpots.Select(fs => new FishingSpotDto
            {
                Id = fs.Id,
                Name = fs.Name,
                Coordinates = fs.Coordinates,
                FishTypes = fs.FishTypes,
                Rating = fs.Rating
            }).ToList();

            return Ok(fishingSpotDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FishingSpotDto>> GetFishingSpot(int id)
        {
            var fishingSpot = await _fishingSpotRepository.GetByIdAsync(id);
            if (fishingSpot == null)
            {
                return NotFound();
            }

            var fishingSpotDto = new FishingSpotDto
            {
                Id = fishingSpot.Id,
                Name = fishingSpot.Name,
                Coordinates = fishingSpot.Coordinates,
                FishTypes = fishingSpot.FishTypes,
                Rating = fishingSpot.Rating
            };

            return Ok(fishingSpotDto);
        }

        [HttpPost]
        public async Task<ActionResult<FishingSpotDto>> CreateFishingSpot(FishingSpotDto fishingSpotDto)
        {
            var fishingSpot = new FishingSpot
            {
                Name = fishingSpotDto.Name,
                Coordinates = fishingSpotDto.Coordinates,
                FishTypes = fishingSpotDto.FishTypes,
                Rating = fishingSpotDto.Rating
            };

            await _fishingSpotRepository.AddAsync(fishingSpot);

            fishingSpotDto.Id = fishingSpot.Id;

            return CreatedAtAction(nameof(GetFishingSpot), new { id = fishingSpotDto.Id }, fishingSpotDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFishingSpot(int id, FishingSpotDto fishingSpotDto)
        {
            var fishingSpot = await _fishingSpotRepository.GetByIdAsync(id);
            if (fishingSpot == null)
            {
                return NotFound();
            }

            fishingSpot.Name = fishingSpotDto.Name;
            fishingSpot.Coordinates = fishingSpotDto.Coordinates;
            fishingSpot.FishTypes = fishingSpotDto.FishTypes;
            fishingSpot.Rating = fishingSpotDto.Rating;

            await _fishingSpotRepository.UpdateAsync(fishingSpot);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFishingSpot(int id)
        {
            var fishingSpot = await _fishingSpotRepository.GetByIdAsync(id);
            if (fishingSpot == null)
            {
                return NotFound();
            }

            await _fishingSpotRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
