using DataAnnotationsExtensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Requests
{
    public class AuthorizeDockingRequest
    {
        [JsonProperty("idBuque")]
        [Required]
        [Min(1, ErrorMessage = "Ingrese un Id de buque valido")]
        public required int IdBuque { get; set; }

        [JsonProperty("nombre")]
        [Required]
        [MinLength(4, ErrorMessage = "Ingrese un nombre de buque valido de +4 caracteres")]
        public required string Nombre { get; set; }

        [JsonProperty("empresa")]
        [Required]
        [MinLength(4, ErrorMessage = "Ingrese un nombre de empresa valido de +4 caracteres")]
        public required string Empresa { get; set; }

        [JsonProperty("muelle")]
        [Required]
        public required int Muelle{ get; set; }
                

        [JsonProperty("fechaHora")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="El campo FechaHora es requerido")]
        [UnixTimestamp(ErrorMessage = "El valor proporcionado no es un timestamp de Unix válido.")]
        public long FechaHora { get; set; }

        public DateTime FechaHoraDateTime => DateTimeOffset.FromUnixTimeSeconds(FechaHora).UtcDateTime;
    }
}
