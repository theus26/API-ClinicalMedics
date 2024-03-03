using API_ClinicalMedics.Domain.Entities;
using FluentValidation;

namespace API_ClinicalMedics.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        TEntity Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
        void Delete(int id);
        IQueryable<TEntity> Get();
        TEntity GetById(int id);
        TEntity Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
    }
}
