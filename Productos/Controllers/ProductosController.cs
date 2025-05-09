using System.Formats.Asn1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos.Models;

namespace Productos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        
        
           private readonly ProductosContext _context;

        public ProductosController(ProductosContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> CrearProducto(Producto product0)
        {
            await _context.Productos.AddAsync(product0);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Producto>>>ListaProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }



        [HttpPut("Actualizar/{id}")]
        //este método sirve para que el parámetro id se obtenga desde la URL en lugar de esperar que se envíe en el cuerpo de la solicitud.

        public async Task<IActionResult> ActualizarProducto([FromRoute] int id, [FromBody] Producto producto)
        {
            // Buscar el producto a actualizar por su id en la base de datos
            var productoExistente = await _context.Productos.FindAsync(id);
            if (productoExistente == null)
            {
                return NotFound(); // Devuelve un error 404 si el producto no existe
            }

            // Actualizar las propiedades del producto existente
            productoExistente.Nombre = producto.Nombre;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Precio = producto.Precio;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok(productoExistente);
        }
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarProducto([FromRoute] int id)
        {
            // Buscar el producto por su ID
            var productoBorrado = await _context.Productos.FindAsync(id);

            // Validar si el producto existe antes de intentar eliminarlo
            if (productoBorrado == null)
            {
                return NotFound(new { mensaje = "No se encontró el producto con la ID especificada." });
            }

            _context.Productos.Remove(productoBorrado);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Producto eliminado con éxito", producto = productoBorrado });
        }
    }
}
        
