using API_ClinicalMedics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_ClinicalMedics.Infra.Data.Mapping
{
    public class AttachamentMap : IEntityTypeConfiguration<Attachaments>
    {
        public void Configure(EntityTypeBuilder<Attachaments> builder)
        {
            builder.ToTable("Attachaments");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.ContentPDF)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("ContentPDF")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.TypeDocument)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("TypeDocument")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.FileName)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("FileName")
                .HasColumnType("varchar(100)");

            builder.HasOne<Users>()
                .WithMany()
                .HasForeignKey(a => a.IdUser)
                .IsRequired();

        }
    }
}
