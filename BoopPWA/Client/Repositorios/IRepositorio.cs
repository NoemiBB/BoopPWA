using BoopPWA.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoopPWA.Client.Repositorios
{
    public interface IRepositorio
    {
        List<Alumno> ObtenerAlumnos();
    }
}
