using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACuyitoApi.Model
{
    public class Completion
    {
        public int? MovimientoID { get; set; }
        public int UsuarioID { get; set; }
        public int? CompletionID { get; set; }
        public ModeloLLM Modelo { get; set; }
        public DateTime FechaCompletion { get; set; }
        public int? InputTokensCacheHit { get; set; }
        public int? InputTokensCacheMiss { get; set; }
        public int? TotalInputTokens { get; set; }
        public int? TotalOutputTokens { get; set; }
        public int TotalTokens { get; set; }
        public double? CostoUnitarioCacheHit { get; set; }
        public double? CostoUnitarioCacheMiss { get; set; }
        public double? CostoUnitarioInput { get; set; }
        public double? CostoUnitarioOutput { get; set; }
        public double CostoEstandar { get; set; }
        public double? CostoReal { get; set; }
        public double? MargenCacheHit { get; set; }
        public double? MargenCacheMiss { get; set; }
        public double? MargenInput { get; set; }
        public double? MargenOutput { get; set; }
        public int UsuarioCreacionID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
