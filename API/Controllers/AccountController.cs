using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Interfaces;
using App.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context,ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")] //api url api/user/register

    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExist(registerDto.Username)) return BadRequest("User already taken");

        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            Username = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return new UserDto
        {
            Username = user.Username,
            Token = tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")] //api url api/user/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x =>
         x.Username == loginDto.Username.ToLower());

        if (user == null) return Unauthorized("Invalid User");
        var hmac = new HMACSHA512(user.PasswordSalt);
        var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedhash.Length; i++)
        {
            if (computedhash[i] != user.PasswordHash[i]) return Unauthorized("Wrong password");
        }
        return new UserDto
        {
            Username = user.Username,
            Token = tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExist(string username)
    {
        return await context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
    }
}