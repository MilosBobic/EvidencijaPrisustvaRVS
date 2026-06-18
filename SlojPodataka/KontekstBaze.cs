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

        public DbSet<UcenikPredmet> UceniciPredmeti { get; set; }

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

            modelBuilder.Entity<Prisustvo>()
                .HasKey(p => new { p.UcenikId, p.CasId });

            modelBuilder.Entity<UcenikPredmet>()
                .HasOne(x => x.Ucenik)
                .WithMany(x => x.UceniciPredmeti)
                .HasForeignKey(x => x.UcenikId);

            modelBuilder.Entity<UcenikPredmet>()
                .HasOne(x => x.Predmet)
                .WithMany(x => x.UceniciPredmeti)
                .HasForeignKey(x => x.PredmetId);
            modelBuilder.Entity<Predmet>()
                .HasOne(p => p.Korisnik)
                .WithMany(k => k.Predmeti)
                .HasForeignKey(p => p.KorisnikId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}