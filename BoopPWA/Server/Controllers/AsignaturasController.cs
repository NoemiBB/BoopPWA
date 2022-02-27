using BoopPWA.Server.Helpers;
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
    public class AsignaturasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public AsignaturasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Asignatura>>> Get([FromQuery] Paginacion paginacion)
        {
            var queryable = context.Asignaturas.AsQueryable()
                                                .OrderBy(x => x.Nombre)
                                                .AsNoTracking();
            if (paginacion.Pagina == 0)
            {
                return await queryable.ToListAsync();
            } else
            {
                await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);
                return await queryable.Paginar(paginacion).ToListAsync();
            }
        }
        //[HttpGet]
        //public async Task<ActionResult<List<Asignatura>>> Get()
        //{
        //    return await context.Asignaturas.OrderBy(x => x.Nombre)
        //                                    .ToListAsync();
        //}   
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignatura>> Get(int id)
        {
            return await context.Asignaturas.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Asignatura asignatura)
        {
            context.Add(asignatura);
            await context.SaveChangesAsync();
            return asignatura.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Asignatura asignatura)
        {
            context.Attach(asignatura).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Asignaturas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Asignatura { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
