using System.ComponentModel.DataAnnotations;

namespace WienerOsiguranjeTehnickiZadatak.Models
{
    public class Domicilnost
    {
        [Key]
        public int Id { get; set; }
        public string? Tip { get; set; }
    }
}
