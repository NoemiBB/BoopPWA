using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoopPWA.Shared.Entidades
{
    public class Examen
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public int AsignaturaId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Fecha { get; set; }
        public string Nota { get; set; }
        public string Tema { get; set; }
        [MaxLength(600)]
        public string Observaciones { get; set; }
        public Alumno Alumno { get; set; }
        public Asignatura Asignatura { get; set; }
        //public bool conNota {
        //    get {
        //        return Nota > 0;
        //    } 
        //}
    }
}
