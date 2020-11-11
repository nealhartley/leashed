using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using leashApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Diagnostics;

namespace leashApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("TestApp")] 
    public class UserController : Controller
    {
        private readonly ParkContext _context;

        public UserController(ParkContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(){

            var token = await HttpContext.GetTokenAsync("access_token");
            var securityTokenHandler = new JwtSecurityTokenHandler();
            var decriptedToken = securityTokenHandler.ReadJwtToken(token);
            var claims = decriptedToken.Claims;
            var sub = claims.First(c => c.Type == "sub").Value;
            var user = await _context.UserData.Where(x => x.TokenSub == sub).FirstOrDefaultAsync();
            Console.WriteLine("---------------------sub-----------------------");
            Console.WriteLine(sub);
           if (user == null)
           {
                return StatusCode(403, $"No User Data: {sub} ");
            }


            return Ok(user);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(String id){

            var user = await _context.UserData.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }


            return Ok(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserData>> Postuser()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var securityTokenHandler = new JwtSecurityTokenHandler();
            var decriptedToken = securityTokenHandler.ReadJwtToken(token);
            var claims = decriptedToken.Claims;
            var sub = claims.First(c => c.Type == "sub").Value;
            var user = await _context.UserData.Where(x => x.TokenSub == sub).FirstOrDefaultAsync();
            Console.WriteLine("---------------------sub-----------------------");
            Console.WriteLine(sub);
            Console.WriteLine("---------------------USER FROM POST-----------------------");
            Console.WriteLine(user);
            if (user != null)
            {
                return StatusCode(403, $"User with auth0 sub, already exists: {sub} {user}");
            }

            user = new UserData
            {
                TokenSub = sub,
                Name = "Not Set Yet"
            };

            _context.UserData.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostUserById", new { id = user.Id }, user);
        }

        [HttpPost("{id}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<ActionResult<UserData>> PostUserById(string sub)
        {
            var user = await _context.UserData.FindAsync(sub);
            if (user != null)
            {
                return StatusCode(403, "User with auth0 sub, already exists.");
            }
            user = new UserData
            {
                TokenSub = sub
            };

            _context.UserData.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostUserById", new { id = user.Id }, user);
        }

     
    }
}