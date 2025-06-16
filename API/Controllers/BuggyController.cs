using API.Data;
using App.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Controllers;


public class BuggyController(DataContext context) : BaseApiController
{
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GeTAuth()
    {
        return "Secret Text";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GeTNotFound()
    {
        var thing = context.Users.Find(-1);
        if (thing == null) return NotFound();
        return thing;
    }

    [HttpGet("server-error")]
    public ActionResult<AppUser> GeTServerError()
    {
        var thing = context.Users.Find(-1) ?? throw new Exception("A bad thing has happen");
        return thing;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GeTBadRequest(){
        return BadRequest("This is a bad request");
    }


}