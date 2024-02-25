using API_ClinicalMedics.Domain.Entities;
using API_ClinicalMedics.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace API_ClinicalMedics.Infra.Data.Context
{
    public class ClinicalsMedicsContext : DbContext
    {

        public ClinicalsMedicsContext(DbContextOptions<ClinicalsMedicsContext> options) : base(options)
        {

        }
        public DbSet<Attachaments> Attachaments { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>(new UserMap().Configure);
            modelBuilder.Entity<Attachaments>(new AttachamentMap().Configure);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("CLINICALS_MEDICS_CONNECTION");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}
