using Interface;
using ItemProjLast.Domian.Dto;
using ItemProjLast.Domian.Enums;
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

    [HttpGet]
    [Route("/GetById:{id}")]
    public async ValueTask<ActionResult<User>> GetByIdAsync(int id)
    {
        var entityResoult =await _userRepository.GetById(id);
        if (entityResoult is null)
            BadRequest(nameof(entityResoult));
        return entityResoult;
    }

    [HttpPost]
    [Route("/")]
    public async ValueTask<ActionResult<UserDto>> CreateAsync(UserDto userDto)
    {
        if (userDto is null) 
            return BadRequest(nameof(userDto));
        
        var entity = new User()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Age = userDto.Age,
            Gmail = userDto.Gmail,
            Login = userDto.Login,
            Password = userDto.Password,
            Role = Role.Client
        };
        
        var data = await _userRepository.CreateAsync(entity);
        
        var entityResoult = new UserDto()
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            Age = data.Age,
            Gmail = data.Gmail,
            Login = data.Login,
            Password = data.Password
        };
        
        return Ok(entityResoult);
    }

    [HttpPut]
    [Route("/")]
    public async ValueTask<ActionResult<User>> UpdateAsync(User user)
    {
        if (user is null)
            return BadRequest(nameof(user));
        var entityResoult = await _userRepository.UpdateAsync(user);
        return Ok(entityResoult);
    }

    [HttpDelete]
    [Route("/")]
    public async ValueTask<ActionResult<User>> DeleteAsync(int id)
    {
        return  Ok(_userRepository.DeleteAsync(id));
    }
    
    
    
    
    
}