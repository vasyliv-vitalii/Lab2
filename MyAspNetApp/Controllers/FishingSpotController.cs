using AutoMapper;
using BLLayer.Services;
using DALayer.CommandRepositories;
using DALayer.QueryRepositories;
using DomainLayer.Abstarction.ICommandRepositories;
using DomainLayer.Abstarction.IQueryRepositories;
using DomainLayer.Abstarction.IServices;
using DomainLayer.Models;
using FishingAndCyclingApp.DTOs;
using FishingAndCyclingApp.Validators;
using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Validators;

namespace FishingAndCyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FishingSpotsController : ControllerBase
    {

        private readonly IFishingSpotCommandRepository _fishingSpotCommandRepository;
        private readonly IFishingSpotQueryRepository _fishingSpotQueryRepository;
        private readonly IFishingSpotServise _fishingSpotService;
        private readonly IMapper _mapper;

        public FishingSpotsController(IFishingSpotCommandRepository fishingSpotCommandRepository, 
            IFishingSpotQueryRepository fishingSpotQueryRepository, IFishingSpotServise fishingSpotService, IMapper mapper)
        {
            _fishingSpotCommandRepository = fishingSpotCommandRepository;
            _fishingSpotQueryRepository = fishingSpotQueryRepository;
            _fishingSpotService = fishingSpotService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetFishingSpots()
        {
            var fishingSpots = await _fishingSpotQueryRepository.GetAllFishingSpots();
            var fishingSpotDtos = _mapper.Map<List<FishingSpotDto>>(fishingSpots);

            return Ok(fishingSpotDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetFishingSpotById(int id)
        {
            var fishingSpot = await _fishingSpotQueryRepository.GetFishingSpotById(id);
            if (fishingSpot == null)
            {
                return NotFound();
            }

            var fishingSpotDto = _mapper.Map<FishingSpotDto>(fishingSpot);

            return Ok(fishingSpotDto);
        }

        [HttpPost]
        public async Task<ActionResult<FishingSpotDto>> CreateFishingSpot(CreateUpdateFishingSpotDto fishngSpotDto)
        {
            FishingSpotCreateUpdateDtoValidator.ValidateDto(fishngSpotDto);

            var fishingSpot = await _fishingSpotService.CreateFishingSpotAsync(_mapper.Map<FishingSpot>(fishngSpotDto));
            fishingSpot = await _fishingSpotCommandRepository.CreateFishingSpot(fishingSpot);
            return _mapper.Map<FishingSpotDto>(fishingSpot);
        }

        [HttpPut("{id}")]
        public async Task<FishingSpotDto> UpdateFishingSpot(int id, CreateUpdateFishingSpotDto fishingSpotDto)
        {
            FishingSpotCreateUpdateDtoValidator.ValidateDto(fishingSpotDto);

            var fishingSpot = await _fishingSpotService.UpdateFishingSpot(id, _mapper.Map<FishingSpot>(fishingSpotDto));
            fishingSpot = await _fishingSpotCommandRepository.UpdateFishingSpot(fishingSpot);
            return _mapper.Map<FishingSpotDto>(fishingSpot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFishingSpot(int id)
        {
            var fishingSpot = await _fishingSpotService.DeleteFishingSpot(id);
            await _fishingSpotCommandRepository.DeleteFishingSpot(fishingSpot);
            return Ok("FishingSpot was deleted");
        }

    }
}
