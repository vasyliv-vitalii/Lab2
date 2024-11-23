using FishingAndCyclingApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DomainLayer.Abstarction.ICommandRepositories;
using DomainLayer.Abstarction.IQueryRepositories;
using DomainLayer.Abstarction.IServices;
using DomainLayer.Models;
using FishingAndCyclingApp.Validators;

namespace FishingAndCyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController( IUserQueryRepository userQueryRepository,
            IUserCommandRepository userCommandRepository, IMapper mapper, IUserService userService)
        {
            _userQueryRepository = userQueryRepository;
            _userCommandRepository = userCommandRepository;
            _mapper = mapper;
            _userService = userService;
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
            var user = await _userQueryRepository.GetUserById(id);
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
        public async Task<ActionResult<UserDto>> CreateUser(CreateUpdateUserDto userDto)
        {
            UserCreateUpdateDtoValidator.ValidateDto(userDto);
            
            var user = await _userService.CreateUserAsync(_mapper.Map<User>(userDto));
            user = await _userCommandRepository.CreateUser(user);
            return _mapper.Map<UserDto>(user);
        }

        [HttpPut("{id}")]
        public async Task<UserDto> UpdateUser(int id, CreateUpdateUserDto userDto)
        {
            UserCreateUpdateDtoValidator.ValidateDto(userDto);
            var user = await _userService.UpdateUser(id, _mapper.Map<User>(userDto));
            user = await _userCommandRepository.UpdateUser(user);
            return _mapper.Map<UserDto>(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.DeleteUser(id);
            await _userCommandRepository.DeleteUser(user);
            return Ok();
        }
    }
}
