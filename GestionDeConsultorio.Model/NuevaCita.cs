using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeConsultorio.Model
{
    public class NuevaCita
    {
        public int Id { get; set; }

        [Display(Name = "Identificación del paciente")]
        public string IdentificacionPaciente { get; set; }

        [Display(Name = "Nombre del paciente")]
        public string NombrePaciente { get; set; }

        [Display(Name = "Motivo de la Cita")]
        public string MotivoDeLaCita { get; set; }

        [Display(Name = "Fecha de la Cita")]
        [DataType(DataType.Date)]
        public DateTime FechaCita { get; set; }

        public Horarios Hora {  get; set; }

    }
}
