using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACuyitoApi.Model
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Cuenta? Cuenta { get; set; }
    }
}
