using GestionDeConsultorio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeConsultorio.BL
{
    public interface IAdministradorDeCita
    {

        List<Cita> ObtengaLaListaDeCitas();

        void AgregueUnaCita(NuevaCita laCita);

        public void EditeUnaCita(Model.NuevaCita laCitaConLosDatos);

        public Model.NuevaCita ObtengaNuevaCitaPorId(int id);

        bool EstaDisponibleLaFechaYHora(DateTime fechaSolicitada, Horarios HoraSolicitada);

        bool EsDiaDeFinDeSemana(DateTime fechaSolicitada);

        public bool EstaDisponibleLaFechaYHoraParaEditar(DateTime fechaSolicitada, Horarios horaSolicitada, int citaId);

        public Model.Cita ObtengaPorId(int id);

        public List<Cita> ObtengaLaListaDeCitasDeHoy();


        void InicieLaCita(Cita citaCompletada);

        void CanceleLaCita(Cita citaCancelada);
    }
}
