using Microsoft.AspNetCore.Mvc;
using InmobiliariaLorenzo.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InmobiliariaLorenzo.Controllers
{
    [Authorize]
    public class ContratosController : Controller
    {
        private readonly IRepositorioContrato repoContrato;
        private readonly IRepositorioInmueble repoInmuble;
        private readonly IRepositorioInquilino repoInquilino;
        private readonly IConfiguration config;

        public ContratosController(IRepositorioContrato repo, IRepositorioInmueble repIn, IRepositorioInquilino repoInq, IConfiguration config)
        {
            this.repoContrato = repo;
            this.repoInmuble = repIn;
            this.repoInquilino = repoInq;
            this.config = config;
        }

        [Route("[controller]/Index")]
        public ActionResult Index()
        {
            var lista = repoContrato.ObtenerTodos();
            return View(lista);
        }

        public ActionResult Details(int id)
        {
            var contrato = repoContrato.ObtenerPorId(id);
            return View(contrato);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato contrato)
        {
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();

            if (contrato.Fecha_Fin < contrato.Fecha_Inicio)
            {
                ModelState.AddModelError("Fecha_Fin", "La fecha de fin no puede ser anterior a la fecha de inicio.");
                return View(contrato);
            }

            var claim = User.FindFirst("Id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                TempData["Otro"] = "No se pudo identificar el usuario actual.";
                return RedirectToAction(nameof(Index));
            }

            contrato.Id_Usuario_Creador = int.Parse(claim.Value);

            if (repoInmuble.VerificarDisponibilidad(contrato.Id_Inmueble, contrato.Fecha_Inicio, contrato.Fecha_Fin))
            {
                repoContrato.Alta(contrato);
                TempData["creado"] = "Contrato Creado";
            }
            else
            {
                TempData["Otro"] = "No se pudo crear el contrato: no hay disponibilidad.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var contrato = repoContrato.ObtenerPorId(id);
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();
            return View(contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contrato contrato)
        {
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();

            if (contrato.Fecha_Fin < contrato.Fecha_Inicio)
            {
                ModelState.AddModelError("Fecha_Fin", "La fecha de fin no puede ser anterior a la fecha de inicio.");
                return View(contrato);
            }

            repoContrato.Modificacion(contrato);
            TempData["editado"] = "Contrato editado";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(policy: "Administrador")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var contrato = repoContrato.ObtenerPorId(id);
            return View(contrato);
        }

        [Authorize(policy: "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Contrato contrato)
        {
            repoContrato.Baja(id);
            TempData["eliminado"] = "Contrato eliminado";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult ContratosPorInquilino(int id)
        {
            var lista = repoContrato.ObtenerContratosPorInquilino(id);
            return View(lista);
        }

        [HttpGet]
        public ActionResult FinalizarContrato(int id, int id_inquilino = 0)
        {
            var contrato = repoContrato.ObtenerPorId(id);
            ViewBag.id_inquilino = id_inquilino;
            ViewBag.Monto = repoContrato.CalcularMontoCancelacion(id);
            return View(contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinalizarContrato(int id, Contrato contrato, int id_inquilino = 0)
        {
            ViewBag.id_inquilino = id_inquilino;
            var claim = User.FindFirst("Id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            int usuarioId = claim != null ? int.Parse(claim.Value) : 0;
            repoContrato.FinalizarContrato(id, usuarioId);
            TempData["Otro"] = "Contrato Finalizado Correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult ContratosVigentes()
        {
            var lista = repoContrato.ObtenerContratosVigentes();
            return View(lista);
        }

        [HttpGet]
        public ActionResult ContratosInmuebles()
        {
            var lista = repoContrato.ObtenerTodos();
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            return View(lista);
        }

        [HttpPost]
        public ActionResult ContratosInmuebles(ContratoBusqueda cb)
        {
            var lista = repoContrato.ObtenerContratosPorInmueble(cb);
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            return View(lista);
        }

        [HttpGet]
        public ActionResult CreateContrato(int id_inmueble = 0)
        {
            ViewBag.id_inmueble = id_inmueble;
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();
            return View("Create");
        }

        [HttpGet]
        public ActionResult RenovarContrato(int id_inmueble = 0, int id_inquilino = 0)
        {
            ViewBag.id_inmueble = id_inmueble;
            ViewBag.id_inquilino = id_inquilino;
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();
            return View("Create");
        }
    }
}
