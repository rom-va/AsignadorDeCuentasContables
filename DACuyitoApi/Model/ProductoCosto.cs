using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACuyitoApi.Model
{
    public class ProductoCosto
    {
        public int CostoID { get; set; }
        public int ProductoID { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public bool Vigente { get; set; }
        public double Monto { get; set; }
        public double MargenGanancia { get;set; }
        public int UsuarioCreacionID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }

    }
}
