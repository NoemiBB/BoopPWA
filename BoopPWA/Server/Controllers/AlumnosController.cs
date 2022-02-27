using BoopPWA.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoopPWA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public AlumnosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Alumno>>> Get()
        {
            return await context.Alumnos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> Get(int id)
        {
            return await context.Alumnos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Alumno alumno)
        {
            context.Add(alumno);
            await context.SaveChangesAsync();
            return alumno.Id;
        }
        [HttpPut]
        public async Task<ActionResult> Put(Alumno alumno)
        {
            context.Attach(alumno).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Alumnos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Alumno { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
