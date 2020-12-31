using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tienda.Models;
using System.Linq;
using System;

namespace Tienda.Controllers
{
    public class Compras : Controller
    {
        TiendaCTX ctx;

        public Compras(TiendaCTX _ctx)
        {
            ctx = _ctx;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Compras = await ctx.Compras.Include("DetalleCompras").Include("Proveedor").ToListAsync();
            return View();
        }

        public async Task<IActionResult> Nuevo()
        {
            ViewBag.Proveedores = await ctx.Proveedores.ToListAsync();
            ViewBag.Productos = await ctx.Productos.Select(x=>new {x.IdProducto, NombreProducto = x.Nombre + " - " + x.Marca}).ToListAsync();
            Tuple<Compra, DetalleCompra> Model = new Tuple<Compra, DetalleCompra>(new Compra(), new DetalleCompra());
            return View(Model);
            //return View(new Compra());
        }
    }
}