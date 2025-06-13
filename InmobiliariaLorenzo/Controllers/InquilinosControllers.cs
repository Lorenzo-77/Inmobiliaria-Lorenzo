using InmobiliariaLorenzo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaLorenzo.Controllers;

[Authorize]
public class InquilinosController : Controller
{
	private readonly IRepositorioInquilino repositorio;
	private readonly IConfiguration config;

	public InquilinosController(IRepositorioInquilino repo, IConfiguration config)
	{
		this.repositorio = repo;
		this.config = config;
	}

	// GET: Inquilino
	[Route("[controller]/Index")]
	public ActionResult Index()
	{
		var lista = repositorio.ObtenerTodos();
		ViewBag.Id = TempData["Id"];
		if (TempData.ContainsKey("Mensaje"))
			ViewBag.Mensaje = TempData["Mensaje"];
		return View(lista);
	}
	// GET: Inquilino/Details/5
	public ActionResult Details(int id)
	{
		var lista = repositorio.ObtenerPorId(id);
		ViewBag.Mensaje = TempData["Mensaje"];
		return View(lista);
	}

	// GET: Inquilino/Edit/5
	public ActionResult Edit(int id)
	{
		var entidad = repositorio.ObtenerPorId(id);
		return View(entidad);//pasa el modelo a la vista
	}

	// POST: Inquilino/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public ActionResult Edit(int id, Inquilino entidad)
	{
		Inquilino? p = null;
		p = repositorio.ObtenerPorId(id);
		////////////////////////////////////////
		p.Nombre = entidad.Nombre;
		p.Apellido = entidad.Apellido;
		p.Dni = entidad.Dni;
		p.Telefono = entidad.Telefono;
		repositorio.Modificacion(p);
		TempData["editado"] = "Si";
		return RedirectToAction(nameof(Index));
	}

	// GET: Inquilino/Create
	public ActionResult Create()
	{
		return View();
	}
	// POST: Inquilino/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public ActionResult Create(Inquilino inquilino)
	{
		if (ModelState.IsValid)// Pregunta si el modelo es válido
		{
			repositorio.Alta(inquilino);
			TempData["Id"] = inquilino.Id_Inquilino;
			TempData["creado"] = "Si";
			return RedirectToAction(nameof(Index));
		}
		else
			return View(inquilino);
	}

	// GET: Inquilino/Delete/5
	[Authorize(policy: "Administrador")]
	[HttpGet]

	public ActionResult Delete(int id)
	{
		var entidad = repositorio.ObtenerPorId(id);
		return View(entidad);
	}

	// POST: Inquilino/Delete/5
	[Authorize(policy: "Administrador")]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public ActionResult Delete(int id, Propietario entidad)
	{
		repositorio.Baja(id);
		TempData["eliminado"] = "Si";
		return RedirectToAction(nameof(Index));
	}
}