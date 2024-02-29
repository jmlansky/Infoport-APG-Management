using Applications.Enums;

namespace Infraestructure.Requests
{
    public class AuthorizationDockingResponse
    {
        public int IdDocking { get; set; }
        public string Status { get; set; } = string.Empty;
        public int IdBuque { get; set; }
        public string NombreBuque { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public int Muelle { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
