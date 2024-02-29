using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Buque
    {
        public int Id { get; set; }
        public string Empresa { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;        
    }
}
