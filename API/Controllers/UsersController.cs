using API.DTOs;
using API.Entities;
using API.Interfaces;
using App.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

 [Authorize]
public class UsersController(IUserRepository userRepository,IMapper mapper) : BaseApiController
{

  [HttpGet]
  public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
  {
    var users = await userRepository.GetUsersAsync();
    if (users == null) return NotFound();
    var returnmapuser = mapper.Map<IEnumerable<MemberDto>>(users);

    return Ok(returnmapuser);
  }

  [HttpGet("{username}")]
  public async Task<ActionResult<MemberDto>> GetUser(string username)
  {
    var user = await userRepository.GetUserByUsernameAsync(username);
    if (user == null) return NotFound();

    return mapper.Map<MemberDto>(user);
  }
}

