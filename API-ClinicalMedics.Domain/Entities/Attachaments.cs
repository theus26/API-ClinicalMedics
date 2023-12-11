using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_ClinicalMedics.Domain.Entities
{
    public class Attachaments
    {
        [Key]
        public long IdAttachament { get; set; }
        [Required]
        public string TipoDocumento { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContentPDF { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public long IdUser { get; set; }

        [ForeignKey("IdUser")]
        public virtual Users User { get; set; }
    }
}
