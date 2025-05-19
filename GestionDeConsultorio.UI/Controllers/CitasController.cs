using GestionDeConsultorio.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionDeConsultorio.Model;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionDeConsultorio.UI.Controllers
{
    public class CitasController : Controller
    {
        public readonly IAdministradorDeCita ElAdministrador;

        public CitasController(IAdministradorDeCita administrador)
        {
            ElAdministrador = administrador;
        }

        // GET: CitasController
        public ActionResult Index(string nombre)
        {
            List<Cita> laLista;
            laLista = ElAdministrador.ObtengaLaListaDeCitas();



            if (nombre is null)
                return View(laLista);
            else
            {
                List<Model.Cita> listaFiltrada;
                listaFiltrada = laLista.Where(x => x.NombrePaciente.Contains(nombre)).ToList();

                return View(listaFiltrada);
            }
        }

        // GET: CitasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CitasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CitasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NuevaCita nuevaCita)
        {
            try
            {
                bool disponible;
                disponible = ElAdministrador.EstaDisponibleLaFechaYHora(nuevaCita.FechaCita, nuevaCita.Hora);

                bool finDeSemana;
                finDeSemana = ElAdministrador.EsDiaDeFinDeSemana(nuevaCita.FechaCita);

                if (finDeSemana)
                {
                    ModelState.AddModelError("FechaCita", "El doctor solo atiende citas de Lunes a Viernes.");
                    return View(nuevaCita);

                }
                else
                {

                    if (!disponible)

                    {

                        ModelState.AddModelError("Hora", "Ya hay una cita programada para esta fecha y hora.");
                        return View(nuevaCita);

                    }

                    ElAdministrador.AgregueUnaCita(nuevaCita);

                    return RedirectToAction(nameof(Index));

                }

                
            }
            catch
            {
                return View();
            }
        }

        // GET: CitasController/Edit/5
        public ActionResult Edit(int id)
        {
            Model.NuevaCita cita;
            cita = ElAdministrador.ObtengaNuevaCitaPorId(id);

            return View(cita);
        }

        // POST: CitasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Model.NuevaCita cita)
        {
            try
            {

                bool disponible;

                disponible = ElAdministrador.EstaDisponibleLaFechaYHoraParaEditar(cita.FechaCita, cita.Hora, cita.Id);

                bool finDeSemana;

                finDeSemana = ElAdministrador.EsDiaDeFinDeSemana(cita.FechaCita);

                if (finDeSemana)
                {
                    ModelState.AddModelError("FechaCita", "El doctor solo atiende citas de Lunes a Viernes.");
                    return View(cita);
                } else
                {

                    if (!disponible)
                    {
                        ModelState.AddModelError("Hora", "Ya hay una cita programada para esta fecha y hora.");
                        return View(cita);
                    }

                    ElAdministrador.EditeUnaCita(cita);
                    return RedirectToAction(nameof(Index));

                }

            }
            catch
            {
                return View();
            }
        }

        // GET: CitasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CitasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
