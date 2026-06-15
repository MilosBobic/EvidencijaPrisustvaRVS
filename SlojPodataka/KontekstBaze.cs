using Microsoft.EntityFrameworkCore;
using SlojPodataka.Modeli;

namespace SlojPodataka
{
    public class KontekstBaze : DbContext
    {
        public KontekstBaze(DbContextOptions<KontekstBaze> options)
            : base(options)
        {
        }

        public DbSet<Ucenik> Ucenici { get; set; }

        public DbSet<Predmet> Predmeti { get; set; }

        public DbSet<Cas> Casovi { get; set; }

        public DbSet<Prisustvo> Prisustva { get; set; }

        public DbSet<Korisnik> Korisnici { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cas>()
                .HasOne(c => c.Predmet)
                .WithMany(p => p.Casovi)
                .HasForeignKey(c => c.PredmetId);

            modelBuilder.Entity<Prisustvo>()
                .HasOne(p => p.Ucenik)
                .WithMany(u => u.Prisustva)
                .HasForeignKey(p => p.UcenikId);

            modelBuilder.Entity<Prisustvo>()
                .HasOne(p => p.Cas)
                .WithMany(c => c.Prisustva)
                .HasForeignKey(p => p.CasId);
        }
    }
}