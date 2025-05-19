using GestionDeConsultorio.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionDeConsultorio.UI.Controllers
{
    public class CitasDelDiaController : Controller
    {

        public readonly IAdministradorDeCita ElAdministrador;

        public CitasDelDiaController(IAdministradorDeCita administrador)
        {
            ElAdministrador = administrador;
        }

        // GET: CitasDelDiaController
        public ActionResult Index()
        {
            List<Model.Cita> lista;
            lista = ElAdministrador.ObtengaLaListaDeCitasDeHoy();

            return View(lista);
        }


        public ActionResult Iniciar(int id)
        {
            Model.Cita laCita = ElAdministrador.ObtengaPorId(id);
            return View(laCita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Iniciar(Model.Cita laCita)
        {
            try
            {
                ElAdministrador.InicieLaCita(laCita);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Cancelar(int id)
        {
            Model.Cita laCita = ElAdministrador.ObtengaPorId(id);
            return View(laCita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancelar(Model.Cita laCita)
        {
            try
            {
                ElAdministrador.CanceleLaCita(laCita);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
