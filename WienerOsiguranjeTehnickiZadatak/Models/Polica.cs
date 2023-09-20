using System.ComponentModel.DataAnnotations;

namespace WienerOsiguranjeTehnickiZadatak.Models
{
    public class Polica
    {
        [Key]
        public int Id { get; set; }
        private string brojPolice;
        public string BrojPolice
        {
            get { return brojPolice; }
            set
            {
                if (value != null && value.Length == 10)
                    brojPolice = value;
                else
                    throw new ArgumentException("Broj police mora imati 10 znakova!");
            }
        }

        public decimal IznosPremije { get; set; }
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }
    }
}
