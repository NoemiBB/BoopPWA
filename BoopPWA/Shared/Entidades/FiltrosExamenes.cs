using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoopPWA.Shared.Entidades
{
    public class Filtros
    {
        public string AlumnoId { get; set; }
        public string AsignaturaId { get; set; }
        public DateTime Fecha { get; set; } = new DateTime(2020,1,1);
    }
}
