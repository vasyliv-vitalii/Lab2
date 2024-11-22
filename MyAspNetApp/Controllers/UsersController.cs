using FishingAndCyclingApp.DTOs;
using FishingAndCyclingApp.Models;
using FishingAndCyclingApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Abstarction.ICommandRepositories;
using DomainLayer.Abstarction.IQueryRepositories;

namespace FishingAndCyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;

        public UsersController( IUserQueryRepository userQueryRepository,
            IUserCommandRepository userCommandRepository, IMapper mapper)
        {
            _userQueryRepository = userQueryRepository;
            _userCommandRepository = userCommandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userQueryRepository.GetAllUsers();
            var userDtos = _mapper.Map<List<UserDto>>(users);

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userQueryRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            // Тут може бути валідація
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Role = userDto.Role
            };

            await _userRepository.AddAsync(user);

            userDto.Id = user.Id;

            return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Role = userDto.Role;

            await _userRepository.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
