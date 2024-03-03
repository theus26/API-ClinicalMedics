namespace API_ClinicalMedics.Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual string CPF { get; set; }
    }
}
