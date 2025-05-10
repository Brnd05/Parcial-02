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

        // Declara una variable privada de solo lectura para almacenar el contexto de la base de datos
        private readonly ProductosContext _context;

        // Constructor del controlador "ProductosController"
        // Recibe una instancia de "ProductosContext" mediante inyección de dependencias
        public ProductosController(ProductosContext context)
        {
            // Asigna el contexto de la base de datos a la variable privada "_context"
            // Esto permite acceder a la base de datos dentro del controlador
            _context = context;
        }

        [HttpPost] // Indica que este método manejará solicitudes HTTP POST
        [Route("Crear")] // Define la ruta de acceso para la creación de productos
        public async Task<IActionResult> CrearProducto(Producto product0) // Recibe un objeto Producto en la solicitud
        {
            // Agrega el nuevo producto a la base de datos de manera asíncrona
            await _context.Productos.AddAsync(product0);

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Devuelve una respuesta HTTP 200 (OK) indicando que se creó correctamente
            return Ok();
        }

        [HttpGet] // Indica que este método manejará solicitudes HTTP GET
        [Route("lista")] // Define la ruta de acceso como "lista"
        public async Task<ActionResult<IEnumerable<Producto>>> ListaProductos() // Devuelve una lista de productos en formato JSON
        {
            // Obtiene todos los productos de la base de datos de manera asíncrona
            var productos = await _context.Productos.ToListAsync();

            // Devuelve los productos obtenidos en formato JSON con código de respuesta 200 (OK)
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
        
