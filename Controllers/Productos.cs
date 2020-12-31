using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda.Models;


namespace Tienda.Controllers
{
    public class Productos : Controller
    {
        TiendaCTX ctx;
        public Productos(TiendaCTX _ctx)
        {
            ctx = _ctx;
        }
        public async Task<IActionResult> Index()
        {
            List<Producto> Productos = await ctx.Productos.Include("Categoria").ToListAsync();
            Producto Producto = new Producto();
            ViewBag.Productos = Productos;
            List<Categoria> Categorias = await ctx.Categorias.OrderBy(x=>x.sCategoria).ToListAsync();
            ViewBag.Categorias = Categorias;
            return View(Producto);
        }

        [BindProperty]
        public Producto Producto { get; set; }
        public async Task<IActionResult> SetProducto()
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var _Producto = await ctx.Productos.Where(x=>x.IdProducto == Producto.IdProducto).AnyAsync();
            if(!_Producto) {
                ctx.Productos.Add(Producto);
            }
            else{
                ctx.Productos.Update(Producto);
            }
            await ctx.SaveChangesAsync();
            return Redirect("/Productos");
        }

        public async Task<IActionResult> Modificar(int id)
        {
            var Producto = await ctx.Productos.FindAsync(id);
            if(Producto == null){
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = await ctx.Categorias.OrderBy(x=>x.sCategoria).ToListAsync();
            return View(Producto);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var _Producto = await ctx.Productos.FindAsync(id);
            if(_Producto == null)
            {
                return RedirectToAction("Index");
            }
            else{
                ctx.Productos.Remove(_Producto);
            }
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}