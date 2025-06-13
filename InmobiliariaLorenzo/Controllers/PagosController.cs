using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InmobiliariaLorenzo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaLorenzo.Controllers
{
   [Authorize]
    public class PagosController : Controller
    {
        private readonly IRepositorioPago repoPago;
        private readonly IConfiguration config;

        public PagosController(IRepositorioPago repo, IConfiguration config)
        {
            this.repoPago = repo;
            this.config = config;
        }

        // GET: Pagos
        [Route("[controller]/Index")]
        public ActionResult Index()
        {
            var Lista = repoPago.ObtenerTodos();
            return View(Lista);
        }

        // GET: Pagos/Details/5
        public ActionResult Details(int id)
        {
            var Lista = repoPago.ObtenerPorId(id);
            return View(Lista);
        }

        // GET: Pagos/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Pagos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago pago)
        {
            repoPago.Alta(pago);
            TempData["creado"] = "Si";
            return RedirectToAction(nameof(Index));
        }


        // GET: Pagos/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Lista = repoPago.ObtenerPorId(id);
            return View(Lista);
        }

        // POST: Pagos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pago pago)
        {
            repoPago.Modificacion(pago);
            TempData["editado"] = "Si";
            return RedirectToAction(nameof(Index));
        }

        // GET: Pagos/Delete/5
        [Authorize(policy: "Administrador")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var Lista = repoPago.ObtenerPorId(id);
            return View(Lista);
        }

        // POST: Pagos/Delete/5
        [Authorize(policy: "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pago pago)
        {          
            repoPago.Baja(id);
            TempData["eliminado"] = "Si";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        // GET: Pagos/Create
        public ActionResult PagoPorContrato(int id, int id_inquilino = 0)
        {
            ViewBag.id_inquilino = id_inquilino;          
            ViewBag.Id_Contrato = id;
            ViewBag.ListaPagos = repoPago.PagosContratoPorInquilino(id);
            return View();
        }

        // POST: Pagos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PagoPorContrato(int id, Pago pago)
        {           
            repoPago.Alta(pago);
            return RedirectToAction(nameof(PagoPorContrato), id);
        }
    }
}