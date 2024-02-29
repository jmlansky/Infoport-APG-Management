

using Applications.Enums;

namespace Applications.Models
{
    public class Docking
    {
        public int Id { get; set; }
        public DockingStatus Status { get; set; }
        public Buque Buque { get; set; } = new();
        public int Muelle { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
