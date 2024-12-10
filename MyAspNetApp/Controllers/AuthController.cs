using AutoMapper;
using DomainLayer.Abstraction.ICommandRepositories;
using DomainLayer.Abstraction.IServices;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.DTOs;
using MyAspNetApp.Validators;

namespace MyAspNetApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, IMapper mapper, IUserCommandRepository userCommandRepository, IUserService userService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _mapper = mapper;
        _userCommandRepository = userCommandRepository;
        _userService = userService;
        _logger = logger;
    }
    
    // POST api/<UserController>
    [HttpPost("signup")]
    public async Task<AuthUserDto> SignUp([FromBody] CreateUpdateUserDto userDto)
    {
        UserCreateUpdateDtoValidator.ValidateDto(userDto);
        var user = _mapper.Map<User>(userDto);
        user = await _userService.CreateUserAsync(user);
        user = await _userCommandRepository.CreateUser(user);
        var accessToken = _authService.GenerateAccessToken(user);
        var authUserDto = new AuthUserDto()
        {
            User = _mapper.Map<UserDto>(user),
            AccessToken = accessToken,
        };

        _logger.LogInformation("New user signed up: {Email}", userDto.Email);
        return authUserDto;
        
    }

    [HttpPost("login")]
    public async Task<AuthUserDto> LoginAsync([FromBody] LoginUserDto loginInfoDto)
    {
        var userWithToken = await _authService.Authenticate(loginInfoDto.Email, loginInfoDto.Password);
        
        _logger.LogInformation("User logged in: {Email}", loginInfoDto.Email);
        
        return new AuthUserDto()
        {
            User = _mapper.Map<UserDto>(userWithToken.user),
            AccessToken = userWithToken.accessToken
        };
    }
}