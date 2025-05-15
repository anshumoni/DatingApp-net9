using API.Data;
using App.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers;

[ApiController]

[Route("api/[Controller]")]
public class UsersController(DataContext context):ControllerBase {


  [HttpGet]
   public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
     var users = await context.Users.ToListAsync();
     if(users==null) return NotFound();

     return users;
   }

  [HttpGet("{id:int}")]
 
  public async Task<ActionResult<AppUser>> GetUsers(int id){
     var user = await context.Users.FindAsync(id);
     if(user==null) return NotFound();

     return user;
   }
}
