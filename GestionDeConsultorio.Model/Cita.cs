using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeConsultorio.Model
{
    public class Cita
    {

        public int Id { get; set; }
        [Display(Name = "Identificación del paciente")]
        public string IdentificacionPaciente { get; set; }

        [Display(Name = "Nombre del paciente")]
        public string NombrePaciente { get; set; }

        [Display(Name = "Motivo de la Cita")]
        public string MotivoDeLaCita { get; set; }

        [Display(Name = "Fecha y hora de inicio de la cita")]
        public DateTime FechaDeEntrada { get; set; }

        [Display(Name = "Fecha y hora de salida de la cita")]
        public DateTime FechaDeSalida { get; set; }

        public Estado Estado { get; set; }

        [Display(Name = "Diagnostico")]
        [MinLength(1)]
        [MaxLength(255)]
        public string? Diganostico { get; set; }

        [Display(Name = "Motivo de Cancelación")]
        [MinLength(1)]
        [MaxLength(255)]
        public string? MotivoDeCancelacion { get; set; }

        [MinLength(1)]
        [MaxLength(255)]
        public string? Medicamentos {  get; set; }

        public string FechaDeEntradaFormato
        {
            get { return FechaDeEntrada.ToString("dd/MM/yyyy hh:mm tt"); }
        }

        public string FechaDeSalidaFormato
        {
            get { return FechaDeSalida.ToString("dd/MM/yyyy hh:mm tt"); }
        }

        public TimeSpan HoraDeEntrada
        {
            get { return FechaDeEntrada.TimeOfDay; }
        }


    }
}
