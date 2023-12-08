using Interface;
using ItemProjLast.Domian.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ItemProjLast.Controllers;

[Controller]
[Route("User")]
[Authorize]
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
    public async ValueTask<ActionResult<User>> GetByIdAsync([FromBody] int id)
    {
        var entityResoult =await _userRepository.GetById(id);
        if (entityResoult is null)
            BadRequest(nameof(entityResoult));
        return entityResoult;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/")]
    public async ValueTask<ActionResult<User>> CreateAsync([FromBody]User entity)
    {
        try
        {
            if (entity is null)
                return BadRequest("obect is null");

            var entityResoult = await _userRepository.CreateAsync(entity);

            return Ok(entityResoult);
        }
        catch (Exception)
        {
            return BadRequest("bad object");
        }
    }

    [HttpPut]
    [Route("/")]
    public async ValueTask<ActionResult<User>> UpdateAsync([FromBody]User user)
    {
        try
        {
            if (user is null)
                return BadRequest(nameof(user));
            var entityResoult = await _userRepository.UpdateAsync(user);
            if (entityResoult is null)
                return BadRequest("not found");
            return Ok(entityResoult);
        }
        catch (ArgumentNullException)
        {
            return BadRequest(nameof(user));
        }
    }

    [HttpDelete]
    [Route("/")]
    public async ValueTask<ActionResult<User>> DeleteAsync([FromBody]int id)
    {
        try
        {
            return Ok(_userRepository.DeleteAsync(id));
        }
        catch (ArgumentNullException)
        {
            return BadRequest("not fund Data");
        }
    }
}