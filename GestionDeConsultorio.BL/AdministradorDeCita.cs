using GestionDeConsultorio.DA;
using GestionDeConsultorio.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeConsultorio.BL
{
    public class AdministradorDeCita : IAdministradorDeCita
    {
        private DBContexto ElContexto;

        public AdministradorDeCita(DBContexto contexto)
        {

            ElContexto = contexto;

        }

        public void AgregueUnaCita(NuevaCita laCita)
        {
            Cita CitaAAgregar = new Cita();

            CitaAAgregar.MotivoDeLaCita = laCita.MotivoDeLaCita;
            CitaAAgregar.IdentificacionPaciente = laCita.IdentificacionPaciente;
            CitaAAgregar.NombrePaciente = laCita.NombrePaciente;

            // Convierte la hora seleccionada a formato de 24 horas
            DateTime fechaEntrada = CambieElFormatoDeLaFechaYHora(laCita.FechaCita, (int)laCita.Hora, 0);
            CitaAAgregar.FechaDeEntrada = fechaEntrada;
            CitaAAgregar.Estado = Estado.Registrada;
            CitaAAgregar.FechaDeSalida = CambieElFormatoDeLaFechaYHora(fechaEntrada, fechaEntrada.Hour, 59);

            ElContexto.Citas.Add(CitaAAgregar);
            ElContexto.SaveChanges();
        }


        public void EditeUnaCita(Model.NuevaCita laCitaConLosDatos)
        {
            Model.Cita laCitaEditar;
            laCitaEditar = ObtengaPorId(laCitaConLosDatos.Id);

            laCitaEditar.IdentificacionPaciente = laCitaConLosDatos.IdentificacionPaciente;
            laCitaEditar.NombrePaciente = laCitaConLosDatos.NombrePaciente;
            laCitaEditar.MotivoDeLaCita = laCitaConLosDatos.MotivoDeLaCita;

            DateTime fechaEntrada = CambieElFormatoDeLaFechaYHora(laCitaConLosDatos.FechaCita, (int)laCitaConLosDatos.Hora, 0);
            laCitaEditar.FechaDeEntrada = fechaEntrada;
            laCitaEditar.FechaDeSalida = CambieElFormatoDeLaFechaYHora(fechaEntrada, fechaEntrada.Hour, 59);

            ElContexto.Citas.Update(laCitaEditar);
            ElContexto.SaveChanges();
        }

        public Model.Cita ObtengaPorId(int id)
        {
            Model.Cita resultado;

            resultado = ElContexto.Citas.Find(id);

            return resultado;
        }

        public Model.NuevaCita ObtengaNuevaCitaPorId(int id)
        {
            Model.Cita cita;
            NuevaCita resultado = new NuevaCita();

            cita = ElContexto.Citas.Find(id);

            if (cita != null)
            {
                resultado.Id = cita.Id;
                resultado.IdentificacionPaciente = cita.IdentificacionPaciente;
                resultado.NombrePaciente = cita.NombrePaciente;
                resultado.MotivoDeLaCita = cita.MotivoDeLaCita;
                resultado.FechaCita = cita.FechaDeEntrada;
                resultado.Hora = (Horarios)cita.FechaDeEntrada.Hour;
            }
            return resultado;
        }

        public List<Cita> ObtengaLaListaDeCitas()
        {
            var resultado = ElContexto.Citas.ToList();

            return resultado;
        }

        public List<Cita> ObtengaLaListaDeCitasDeHoy()
        {
            var lista = from item in ObtengaLaListaDeCitas()
                        where item.FechaDeEntrada.Date == DateTime.Today
                        select item;

            return lista.ToList();
        }

        public void InicieLaCita(Cita citaCompletada)
        {
            Cita CitaAModificar = ObtengaPorId(citaCompletada.Id);
            CitaAModificar.Diganostico = citaCompletada.Diganostico;
            CitaAModificar.Medicamentos = citaCompletada.Medicamentos;
            CitaAModificar.Estado = Estado.Atendida;


            ElContexto.Citas.Update(CitaAModificar);
            ElContexto.SaveChanges();
        }

        public void CanceleLaCita(Cita citaCancelada)
        {
            Cita CitaAModificar = ObtengaPorId(citaCancelada.Id);
            CitaAModificar.MotivoDeCancelacion = citaCancelada.MotivoDeCancelacion;
            CitaAModificar.Estado = Estado.Cancelado;


            ElContexto.Citas.Update(CitaAModificar);
            ElContexto.SaveChanges();
        }

        public bool EstaDisponibleLaFechaYHora(DateTime fechaSolicitada, Horarios HoraSolicitada)
        {
            bool Disponible;

            var resultado = from item in ObtengaLaListaDeCitas()
                            where item.FechaDeEntrada.Date == fechaSolicitada.Date && 
                            item.FechaDeEntrada.Hour == (int)HoraSolicitada select item;

            if (resultado.IsNullOrEmpty())
            {

                Disponible = true;

            } else
            {

                Disponible = false;

            }

            return Disponible;

        }

        public bool EsDiaDeFinDeSemana(DateTime fechaSolicitada)
        {
            bool finDeSemana;

            DayOfWeek dia = fechaSolicitada.DayOfWeek;

            if (dia == DayOfWeek.Saturday || dia == DayOfWeek.Sunday)
            {

                finDeSemana = true;

            }
            else
            {

                finDeSemana = false;

            }

            return finDeSemana;

        }

        public bool EstaDisponibleLaFechaYHoraParaEditar(DateTime fechaSolicitada, Horarios horaSolicitada, int citaId)
        {
            bool disponible;

            
            var citas = ObtengaLaListaDeCitas().Where(c => c.Id != citaId);

            disponible = !citas.Any(c => c.FechaDeEntrada.Date == fechaSolicitada.Date && c.FechaDeEntrada.Hour == (int)horaSolicitada);

            return disponible;

        }

        public DateTime CambieElFormatoDeLaFechaYHora(DateTime fechaOriginal, int nuevaHora, int nuevosMinutos)
        {
           
            if (nuevaHora >= 1 && nuevaHora <= 4)
            {
                nuevaHora += 12;
            }

            return new DateTime(fechaOriginal.Year, fechaOriginal.Month, fechaOriginal.Day, nuevaHora, nuevosMinutos, 0);
        }


    }
}
