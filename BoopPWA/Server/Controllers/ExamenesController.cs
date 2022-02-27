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
    public class ExamenesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ExamenesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Examen>>> Get([FromQuery] Paginacion paginacion)
        {
            var queryable = context.Examenes.AsQueryable()
                                            .OrderByDescending(x => x.Fecha)
                                            .Include(a => a.Alumno)
                                            .Include(a => a.Asignatura)
                                            .AsNoTracking();

            //Console.WriteLine($"filtros: {filtros.AlumnoId} - {filtros.AsignaturaId} - {filtros.Fecha}");
            // Filtrar
            //if (filtros.AlumnoId != null)
            //{
            //    queryable = queryable.Where(x => x.AlumnoId == Convert.ToInt32(filtros.AlumnoId));
            //}
            //if (filtros.AsignaturaId != null)
            //{
            //    queryable = queryable.Where(x => x.AsignaturaId == Convert.ToInt32(filtros.AsignaturaId));
            //}

            // Paginar
            if (paginacion.Pagina == 0)
            {
                return await queryable.ToListAsync();
            }
            else
            {
                await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);
                return await queryable.Paginar(paginacion).ToListAsync();            
            }
        }

       /* [HttpGet]
        public async Task<ActionResult<List<Examen>>> Get([FromQuery] Filtros filtros)
        {
            Paginacion paginacion = new Paginacion
            {
                Pagina = 1,
                CantidadRegistros = 6,
            };

            var queryable = context.Examenes.AsQueryable()
                                        .OrderByDescending(x => x.Fecha)
                                        .Include(a => a.Alumno)
                                        .Include(a => a.Asignatura)
                                        .Where(x => x.Fecha == filtros.Fecha)
                                        .AsNoTracking();
            if (filtros.AlumnoId != null)
            {
                queryable = queryable.Where(x => x.AlumnoId == Convert.ToInt32(filtros.AlumnoId));
            }
            if (filtros.AsignaturaId != null)
            {
                queryable = queryable.Where(x => x.AsignaturaId == Convert.ToInt32(filtros.AsignaturaId));
            }

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);
            return await queryable.Paginar(paginacion).ToListAsync();

        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<Examen>> Get(int id)
        {
            return await context.Examenes.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Examen examen)
        {
            context.Attach(examen).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Examen examen)
        {
            context.Add(examen);
            await context.SaveChangesAsync();
            return examen.Id;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Examenes.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Examen { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
