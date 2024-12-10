using AutoMapper;
using DomainLayer.Abstraction.ICommandRepositories;
using DomainLayer.Abstraction.IQueryRepositories;
using DomainLayer.Abstraction.IServices;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MyAspNetApp.DTOs;
using MyAspNetApp.Validators;

namespace MyAspNetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IBikeRouteCommandRepository _bikeRouteCommandRepository;
        private readonly IBikeRouteQueryRepository _bikeRouteQueryRepository;
        private readonly IBikeRouteService _bikeRouteService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<RoutesController> _logger;

        public RoutesController(IBikeRouteCommandRepository bikeRouteCommandRepository, 
            IBikeRouteQueryRepository bikeRouteQueryRepository,
            IBikeRouteService bikeRouteService, IMapper mapper, 
            IMemoryCache memoryCache, ILogger<RoutesController> logger)
        {
            _bikeRouteCommandRepository = bikeRouteCommandRepository;
            _bikeRouteQueryRepository = bikeRouteQueryRepository;
            _bikeRouteService = bikeRouteService;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeRouteDTO>>> GetBikeRoutes()
        {
            if (_memoryCache.TryGetValue("bikeRoutes", out List<BikeRouteDTO> cachedBikeRoutes))
            {
                _logger.LogInformation("Bike routes retrieved from cache.");
                return Ok(cachedBikeRoutes);
            }

            _logger.LogInformation("Bike routes not found in cache. Retrieving from database...");
            var bikeRoutes = await _bikeRouteQueryRepository.GetAllBikeRoutes();
            var bikeRoutesDtos = _mapper.Map<List<BikeRouteDTO>>(bikeRoutes);

            _logger.LogInformation("Storing bike routes in cache for future requests.");
            _memoryCache.Set("bikeRoutes", bikeRoutesDtos, TimeSpan.FromMinutes(30));

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

            _logger.LogInformation("Bike route created, clearing cache.");
            _memoryCache.Remove("bikeRoutes");

            return _mapper.Map<BikeRouteDTO>(bikeRoute);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BikeRouteDTO>> UpdateBikeRoute(int id, CreateUpdateBikeRouteDto bikeRouteDto)
        {
            BikeRouteCreateUpdateDtoValidator.ValidateDto(bikeRouteDto);

            var bikeRoute = await _bikeRouteService.UpdateBikeRoute(id, _mapper.Map<BikeRoute>(bikeRouteDto));
            bikeRoute = await _bikeRouteCommandRepository.UpdateBikeRoute(bikeRoute);

            _logger.LogInformation("Bike route updated, clearing cache.");
            _memoryCache.Remove("bikeRoutes");

            return _mapper.Map<BikeRouteDTO>(bikeRoute);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBikeRoute(int id)
        {
            var bikeRoute = await _bikeRouteService.DeleteBikeRoute(id);
            await _bikeRouteCommandRepository.DeleteBikeRoute(bikeRoute);

            _logger.LogInformation("Bike route deleted, clearing cache.");
            _memoryCache.Remove("bikeRoutes");

            return Ok("Bike route was deleted");
        }
    }
}
