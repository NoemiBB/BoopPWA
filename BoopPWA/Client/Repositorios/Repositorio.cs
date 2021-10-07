using BoopPWA.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoopPWA.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        public List<Alumno> ObtenerAlumnos()
        {
            return new List<Alumno>()
            {
                new Alumno() {Id = 1, Nombre = "Pablo",Examenes = { }},
                new Alumno() {Id = 2, Nombre = "Silvia",Examenes = { }},
                new Alumno() {Id = 3, Nombre = "Sara",Examenes = { }},
            };
        }
    }
}
