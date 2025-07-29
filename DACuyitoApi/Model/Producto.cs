using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACuyitoApi.Model
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public UnidadDeMedida UnidadDeMedida { get; set; }
        public int UsuarioCreacionID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
