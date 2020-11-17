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

namespace leashApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("TestApp")] 
    public class TokenController : ControllerBase
    {
        private readonly ParkContext _context;

        public TokenController(ParkContext context)
        {
            _context = context;
        }

        // GET: api/Token
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string[]>> GetToken()
        {
            Console.Out.WriteLine("Get works");
            Console.Error.WriteLine("get works");
            Console.Out.Flush();
            var token = await HttpContext.GetTokenAsync("access_token");
            var securityTokenHandler = new JwtSecurityTokenHandler();
            var decriptedToken = securityTokenHandler.ReadJwtToken(token);
            var claims = decriptedToken.Claims;
            List<string> outArray = new List<string>();
            //var enu = claims.GetEnumerator();
            foreach (Claim c in claims)
            {
                outArray.Add($"{c.Type}: {c.Value} :{c.ValueType}");
            }
            

             return outArray.ToArray();
        }

    }
}
