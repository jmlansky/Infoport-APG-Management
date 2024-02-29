namespace Domain.Models
{
    public class Docking
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Muelle { get; set; }
        public DateTime FechaHora { get; set; }
        public Buque Buque { get; set; } = new();
    }
}
