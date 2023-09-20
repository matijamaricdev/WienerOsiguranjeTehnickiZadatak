using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WienerOsiguranjeTehnickiZadatak.Models;

namespace WienerOsiguranjeTehnickiZadatak.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domicilnost> Domicilnost { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<Polica> Polica { get; set; }
        public DbSet<TipPartnera> TipPartnera { get; set; }
    }
}