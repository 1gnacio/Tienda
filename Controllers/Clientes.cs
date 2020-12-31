using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class Clientes : Controller
    {
        TiendaCTX ctx;

        public Clientes(TiendaCTX _ctx)
        {
            ctx = _ctx;
        }
        public IActionResult Index()
        {
            Cliente cliente = new Cliente();
            ViewBag.Clientes = ctx.Clientes.ToList();
            return View(cliente);
        }
        [BindProperty]
        public Cliente cliente { get; set; }
        public IActionResult Guardar()
        {
            if(!ModelState.IsValid){
                return Redirect("/Clientes/");
            }
            var _Cliente = ctx.Clientes.Find(cliente.IdCliente);
            if(_Cliente == null){
                ctx.Clientes.Add(cliente);
            }else{
                _Cliente.Correo = cliente.Correo;
                _Cliente.Dui = cliente.Dui;
                _Cliente.Nit = cliente.Nit;
                _Cliente.Nombre = cliente.Nombre;
            }
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Modificar(int id)
        {
            var Cliente = ctx.Clientes.Find(id);
            if(Cliente == null){
                return Redirect("/Clientes/");
            }
            return View(Cliente);
        }

        public IActionResult Eliminar(int id)
        {
            var cliente = ctx.Clientes.Find(id);
            if(cliente == null){
                return StatusCode(404);
            }

            ctx.Clientes.Remove(cliente);
            ctx.SaveChanges();
            return Redirect("/Clientes/");
        }
    }
}