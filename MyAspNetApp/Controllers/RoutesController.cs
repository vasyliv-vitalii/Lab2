using FishingAndCyclingApp.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DomainLayer.Abstarction.ICommandRepositories;
using DomainLayer.Abstarction.IQueryRepositories;
using DomainLayer.Abstarction.IServices;
using DALayer.QueryRepositories;
using BLLayer.Services;
using DALayer.CommandRepositories;
using DomainLayer.Models;
using FishingAndCyclingApp.Validators;
using MyAspNetApp.Validators;

namespace FishingAndCyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IBikeRouteCommandRepository _bikeRouteCommandRepository;
        private readonly IBikeRouteQueryRepository _bikeRouteQueryRepository;
        private readonly IBikeRouteService _bikeRouteService;
        private readonly IMapper _mapper;

        public RoutesController(IBikeRouteCommandRepository bikeRouteCommandRepository, IBikeRouteQueryRepository bikeRouteQueryRepository,
            IBikeRouteService bikeRouteSpotService, IMapper mapper)
        {
            _bikeRouteCommandRepository = bikeRouteCommandRepository;
            _bikeRouteQueryRepository = bikeRouteQueryRepository;
            _bikeRouteService = bikeRouteSpotService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeRouteDTO>>> GetBikeRouters()
        {
            var bikeRoutes = await _bikeRouteQueryRepository.GetAllBikeRoutes();
            var bikeRoutesDtos = _mapper.Map<List<BikeRouteDTO>>(bikeRoutes);

            return Ok(bikeRoutesDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BikeRouteDTO>> GetBikeRouteById(int id)
        {
            var bikeRoute = await _bikeRouteQueryRepository.GetBikeRouteById(id);
            if (bikeRoute == null)
            {
                return NotFound();
            }

            var bikeRouteDto = _mapper.Map<BikeRouteDTO>(bikeRoute);

            return Ok(bikeRouteDto);

        }

        [HttpPost]
        public async Task<ActionResult<BikeRouteDTO>> CreateBikeRoute(CreateUpdateBikeRouteDto bikeRouteDto)
        {
            BikeRouteCreateUpdateDtoValidator.ValidateDto(bikeRouteDto);

            var bikeRoute = await _bikeRouteService.CreateBikeRouteAsync(_mapper.Map<BikeRoute>(bikeRouteDto));
            bikeRoute = await _bikeRouteCommandRepository.CreateBikeRoute(bikeRoute);
            return _mapper.Map<BikeRouteDTO>(bikeRoute);
        }

        [HttpPut("{id}")]
        public async Task<UserDto> UpdateUser(int id, CreateUpdateBikeRouteDto bikeRouteDto)
        {
            BikeRouteCreateUpdateDtoValidator.ValidateDto(bikeRouteDto); 

            var bikeRoute = await _bikeRouteService.UpdateBikeRoute(id, _mapper.Map<BikeRoute>(bikeRouteDto));
            bikeRoute = await _bikeRouteCommandRepository.UpdateBikeRoute(bikeRoute);
            return _mapper.Map<UserDto>(bikeRoute);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var bikeRoute = await _bikeRouteService.DeleteBikeRoute(id);
            await _bikeRouteCommandRepository.DeleteBikeRoute(bikeRoute);
            return Ok("User was deleted");
        }

    }
}
