using Interface;
using ItemProjLast.Domian.Dto;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ItemProjLast.Controllers;

[Controller]
[Route("User")]
public class UserController:ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    [Route("/")]
    public async ValueTask<List<User>> GetAllAsync()
    {
        return _userRepository.GetAll().ToList();
    }

    public async ValueTask<ActionResult<UserDto>> CreateAsync(UserDto userDto)
    {
        return null;
    }
    
}