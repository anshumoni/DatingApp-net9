using API.Data;
using App.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers;
public class UsersController(DataContext context):BaseApiController {

  [AllowAnonymous]
  [HttpGet]
   public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
     var users = await context.Users.ToListAsync();
     if(users==null) return NotFound();

     return users;
   }

  [HttpGet("{id:int}")]
 
  [Authorize]
  public async Task<ActionResult<AppUser>> GetUser(int id)
  {
    var user = await context.Users.FindAsync(id);
    if (user == null) return NotFound();

    return user;
  }
}

