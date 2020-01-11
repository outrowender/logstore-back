using System.Collections.Generic;
using System.Threading.Tasks;
using logstore.Data;
using logstore.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using logstore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System;
using logstore.Auth;

namespace logstore.Controllers
{
    [ApiController]
    [Route("v1/user")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> showAsync([FromServices] DataContext _ctx, [FromServices] IMapper _mapper)
        {
            var userId = Convert.ToInt32(User.FindFirst("subject")?.Value);

            var user = await _ctx.Users.AsNoTracking().FirstAsync(x => x.Id == userId);

            var userMap = _mapper.Map<UserViewModel>(user);

            return StatusCode(200, user);
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> storeAsync([FromServices] DataContext _ctx, [FromServices] IMapper _mapper, [FromServices] TokenService _tokenService, [FromBody] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);

                _ctx.Users.Add(user);

                await _ctx.SaveChangesAsync();

                var token = _tokenService.GenerateToken(user);

                return StatusCode(200, new { token });

            }
            else
            {
                return BadRequest(ModelState);
            }

        }

    }
}