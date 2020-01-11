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
using System.Linq;

namespace logstore.Controllers
{
    [ApiController]
    [Route("v1/notes")]
    public class NoteController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> indexAsync([FromServices] DataContext _ctx)
        {
            var userId = Convert.ToInt32(User.FindFirst("subject")?.Value);

            var notes = await _ctx.Notes.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();

            return StatusCode(200, notes);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> storeAsync([FromServices] DataContext _ctx, [FromBody] Note model)
        {
            model.UserId = Convert.ToInt32(User.FindFirst("subject")?.Value);

            if (ModelState.IsValid)
            {
                _ctx.Notes.Add(model);

                await _ctx.SaveChangesAsync();

                return StatusCode(201, new { id = model.Id });
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> deleteAsync([FromServices] DataContext _ctx, int id)
        {
            var userId = Convert.ToInt32(User.FindFirst("subject")?.Value);

            var note = await _ctx.Notes.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (note == null) return BadRequest(new { message = "Task not found or access denied" });

            _ctx.Notes.Remove(note);

            await _ctx.SaveChangesAsync();

            //nada a mostrar
            return StatusCode(204);

        }
    }
}