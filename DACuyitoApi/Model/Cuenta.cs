using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACuyitoApi.Model
{
    public class Cuenta
    {
        public int UsuarioID { get; set; }
        public double BalanceSoles { get; set; }
        public double BalanceDolares { get; set; }
        public int UsuarioCreacionID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? UsuarioModificacionID { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
