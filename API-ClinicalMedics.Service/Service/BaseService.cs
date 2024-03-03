using API_ClinicalMedics.Domain.Entities;
using API_ClinicalMedics.Domain.Interfaces;
using FluentValidation;

namespace API_ClinicalMedics.Service.Service
{
    public class BaseService<TEntity>(IBaseRepository<TEntity> baseRepository) : IBaseService<TEntity>
        where TEntity : BaseEntity
    {
        public TEntity Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {

            Validate(obj, Activator.CreateInstance<TValidator>());
            baseRepository.Insert(obj);
            return obj;
        }

        public TEntity Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            baseRepository.Update(obj);
            return obj;
        }

        public void Delete(int id) => baseRepository.Delete(id);

        public IQueryable<TEntity> Get() => baseRepository.Select();

        public TEntity GetById(int id) => baseRepository.Select(id);

        private static void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}

