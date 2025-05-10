using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Productos.Models
{
    public class Producto
    {
        // Propiedad que representa el identificador único del producto (clave primaria)
        public int Id { get; set; }

        // Propiedad que almacena el nombre del producto
        // Se aplica una restricción de longitud máxima de 50 caracteres con un mensaje de error personalizado
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Nombre { get; set; } = null!;

        // Propiedad que almacena la descripción del producto
        // Se indica que será un campo de texto multilínea
        [DataType(DataType.MultilineText)]
        // Se limita la descripción a un máximo de 500 caracteres con un mensaje de error en caso de superar el límite
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Descripcion { get; set; } = null!;

        // Propiedad que representa el precio del producto
        // Se define el tipo de columna en la base de datos como decimal con una precisión de 18 dígitos y 2 decimales
        [Column(TypeName = "decimal(18,2)")]
        // Se especifica un formato de visualización en moneda para mostrar correctamente el valor monetario
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Precio { get; set; }
    }
}
