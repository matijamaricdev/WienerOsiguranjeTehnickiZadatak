using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WienerOsiguranjeTehnickiZadatak.Models
{
    public class Partner
    {
        [Key]
        public int Id { get; set; }
        public int TipPartneraId { get; set; }
        public TipPartnera TipPartnera { get; set; }
        public int DomicilnostId { get; set; }
        public Domicilnost Domicilnost { get; set; }
        public string OIB { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Naziv { get; set; }
        public char? Spol { get; set; }  // M ili Ž ili null za pravne osobe
        public string Adresa { get; set; }
        public string EksterniBrojPartnera { get; set; }
        public DateTime DatumUnosa { get; set; } = new DateTime();
        [NotMapped]
        public bool SpecialPartner { get; set; } = false; //5+ polica osiguranja i 5000 HRK+
    }


}
