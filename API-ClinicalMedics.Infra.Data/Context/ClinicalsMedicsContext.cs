using API_ClinicalMedics.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_ClinicalMedics.Infra.Data.Context
{
    public class ClinicalsMedicsContext : DbContext
    {
        public DbSet<Attachaments> Attachaments { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("CLINICALS_MEDICS_CONNECTION");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}
