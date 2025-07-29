using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACuyitoApi.Model
{
    public class Movimiento
    {
        public int MovimientoID { get; set; }
        public int UsuarioID { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public double Monto { get; set; }
        public Moneda Moneda { get; set; }
        public TipoMovimiento Tipo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioCreacionID { get; set; }
        public DateTime FechaCreacion {get; set;}
        public int UsuarioModificacionID { get; set; }
        public DateTime FechaModificacion { get; set; }

    }
}
