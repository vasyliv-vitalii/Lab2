using AutoMapper;
using DomainLayer.Abstraction.ICommandRepositories;
using DomainLayer.Abstraction.IQueryRepositories;
using DomainLayer.Abstraction.IServices;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.DTOs;
using MyAspNetApp.Validators;

namespace MyAspNetApp.Controllers
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

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpPut]
        [Authorize]
        public async Task<UserDto> UpdateUser(CreateUpdateUserDto userDto)
        {
            UserCreateUpdateDtoValidator.ValidateDto(userDto);
            var user = await _userService.UpdateUser(_mapper.Map<User>(userDto));
            user = await _userCommandRepository.UpdateUser(user);
            return _mapper.Map<UserDto>(user);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var user = await _userService.DeleteUser();
            await _userCommandRepository.DeleteUser(user);
            return Ok("User was deleted");
        }
    }
}
