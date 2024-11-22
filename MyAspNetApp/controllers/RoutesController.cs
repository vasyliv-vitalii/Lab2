using FishingAndCyclingApp.DTOs;
using FishingAndCyclingApp.Models;
using FishingAndCyclingApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Route = FishingAndCyclingApp.Models.Route;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishingAndCyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRepository<Route> _routeRepository;

        public RoutesController(IRepository<Route> routeRepository)
        {
            _routeRepository = routeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteDto>>> GetRoutes()
        {
            var routes = await _routeRepository.GetAllAsync();
            var routeDtos = routes.Select(r => new RouteDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Distance = r.Distance,
                Difficulty = r.Difficulty
            }).ToList();

            return Ok(routeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDto>> GetRoute(int id)
        {
            var route = await _routeRepository.GetByIdAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            var routeDto = new RouteDto
            {
                Id = route.Id,
                Name = route.Name,
                Description = route.Description,
                Distance = route.Distance,
                Difficulty = route.Difficulty
            };

            return Ok(routeDto);
        }

        [HttpPost]
        public async Task<ActionResult<RouteDto>> CreateRoute(RouteDto routeDto)
        {
            var route = new Route
            {
                Name = routeDto.Name,
                Description = routeDto.Description,
                Distance = routeDto.Distance,
                Difficulty = routeDto.Difficulty
            };

            await _routeRepository.AddAsync(route);

            routeDto.Id = route.Id;

            return CreatedAtAction(nameof(GetRoute), new { id = routeDto.Id }, routeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoute(int id, RouteDto routeDto)
        {
            var route = await _routeRepository.GetByIdAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            route.Name = routeDto.Name;
            route.Description = routeDto.Description;
            route.Distance = routeDto.Distance;
            route.Difficulty = routeDto.Difficulty;

            await _routeRepository.UpdateAsync(route);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var route = await _routeRepository.GetByIdAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            await _routeRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
