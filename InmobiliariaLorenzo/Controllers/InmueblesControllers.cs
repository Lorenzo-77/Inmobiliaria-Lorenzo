using System;
using System.Collections.Generic;
using InmobiliariaLorenzo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace InmobiliariaLorenzo.Controllers;

[Authorize]
public class InmueblesController : Controller
{
    private readonly IRepositorioInmueble repoInmuble;
    private readonly IRepositorioTipo repoTipo;
    private readonly IRepositorioUso repoUso;
    private readonly IRepositorioPropietario repoPropietario;
    private readonly IConfiguration config;

    public InmueblesController(IRepositorioInmueble repo, IRepositorioTipo repTipo, IRepositorioUso repUso, IRepositorioPropietario repoPro, IConfiguration config)
    {
        this.repoInmuble = repo;
        this.repoTipo = repTipo;
        this.repoUso = repUso;
        this.repoPropietario = repoPro;
        this.config = config;
    }

    // GET: Inmuebles
    [Route("[controller]/Index")]
    public ActionResult Index()
    {
        var lista = repoInmuble.ObtenerTodos();
        return View(lista);
    }

    // GET: Inmuebles/Details/5
    [HttpGet]
    public ActionResult Details(int id)
    {
        try
        {
            var inmueble = repoInmuble.ObtenerPorId(id);
            ViewBag.Mensaje = TempData["Mensaje"];
            return View(inmueble);
        }
        catch
        {
            throw;
        }
    }

    // GET: Inmuebles/Create
    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.listaPropietarios = repoPropietario.ObtenerTodos();
        ViewBag.listaTipos = repoTipo.ObtenerTodos();
        ViewBag.listaUsos = repoUso.ObtenerTodos();
        return View();
    }

    // POST: Inmuebles/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Inmueble inmueble)
    {
        repoInmuble.Alta(inmueble);
        TempData["creado"] = "Si";
        return RedirectToAction(nameof(Index));
    }

    // GET: Inmuebles/Edit/5
    [HttpGet]
    public ActionResult Edit(int id)
    {
        ViewBag.listaPropietarios = repoPropietario.ObtenerTodos();
        ViewBag.listaTipos = repoTipo.ObtenerTodos();
        ViewBag.listaUsos = repoUso.ObtenerTodos();

        var inmueble = repoInmuble.ObtenerPorId(id);
        ViewBag.Mensaje = TempData["Mensaje"];

        return View(inmueble);
    }

    // POST: Inmuebles/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Inmueble inmueble)
    {
        repoInmuble.Modificacion(inmueble);
        TempData["editado"] = "Si";
        return RedirectToAction(nameof(Index));
    }

    // GET: Inmuebles/Delete/5
    [Authorize(policy: "Administrador")]
    [HttpGet]
    public ActionResult Delete(int id)
    {
        var inmueble = repoInmuble.ObtenerPorId(id);
        ViewBag.Mensaje = TempData["Mensaje"];
        return View(inmueble);
    }

    // POST: Inmuebles/Delete/5
    [Authorize(policy: "Administrador")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Inmueble inmueble)
    {
        repoInmuble.Baja(id);
        TempData["Eliminado"] = "Si";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public ActionResult BuscarInmuebles()
    {
        var lista = repoInmuble.ObtenerTodos();
        ViewBag.listaTipos = repoTipo.ObtenerTodos();
        ViewBag.listaUsos = repoUso.ObtenerTodos();
        ViewBag.Mensaje = TempData["Mensaje"];
        return View(lista);
    }

    [HttpPost]
    public ActionResult BuscarInmuebles(InmuebleBusqueda ib)
    {
        var lista = repoInmuble.BuscarInmuebles(ib);
        ViewBag.listaTipos = repoTipo.ObtenerTodos();
        ViewBag.listaUsos = repoUso.ObtenerTodos();
        ViewBag.Mensaje = TempData["Mensaje"];
        return View(lista);
    }

    [HttpGet]
    public ActionResult InmueblesEstados()
    {
        ViewBag.listaPropietarios = repoPropietario.ObtenerTodos();
        var lista = repoInmuble.ObtenerTodos();
        ViewBag.Mensaje = TempData["Mensaje"];
        return View(lista);
    }

    [HttpPost]
    public ActionResult InmueblesEstados(InmuebleBusqueda ib)
    {
        ViewBag.listaPropietarios = repoPropietario.ObtenerTodos();
        var lista = repoInmuble.BuscarInmueblesEstado(ib);
        return View(lista);
    }

    [HttpGet]
    public ActionResult InmueblesContratos()
    {
        var lista = repoInmuble.ObtenerTodos();
        return View(lista);
    }

    [HttpPost]
    public ActionResult InmueblesContratos(InmuebleBusqueda ib)
    {
        var lista = repoInmuble.BuscarInmuebles(ib);
        return View(lista);
    }
}
