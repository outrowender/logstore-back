using System.Collections.Generic;
using System.Threading.Tasks;
using logstore.Data;
using logstore.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using logstore.Models.ViewModels;
using logstore.Auth;
using Microsoft.AspNetCore.Authorization;

namespace logstore.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : ControllerBase
    {

        [HttpPost]
        [Route("")]
        [AllowAnonymous]

        public async Task<IActionResult> loginAsync([FromServices] DataContext _ctx, [FromServices] IMapper _mapper, [FromServices] TokenService _tokenService, [FromBody] UserAuthViewModel model)
        {

            var user = _mapper.Map<User>(model);

            var cred = await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);

            if (cred == null) return NotFound(new { message = "Invalid email or password" });

            var token = _tokenService.GenerateToken(cred);

            return StatusCode(200, new { token });

        }
    }
}