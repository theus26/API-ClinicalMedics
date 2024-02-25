using API_ClinicalMedics.Domain.Entities;
using API_ClinicalMedics.Domain.Interfaces;
using API_ClinicalMedics.Infra.Data.Context;

namespace API_ClinicalMedics.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ClinicalsMedicsContext _clinicalsMedicsContext;

        public BaseRepository(ClinicalsMedicsContext mySqlContext)
        {
            _clinicalsMedicsContext = mySqlContext;
        }

        public void Insert(TEntity obj)
        {
            _clinicalsMedicsContext.Set<TEntity>().Add(obj);
            _clinicalsMedicsContext.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _clinicalsMedicsContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _clinicalsMedicsContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _clinicalsMedicsContext.Set<TEntity>().Remove(Select(id));
            _clinicalsMedicsContext.SaveChanges();
        }

        public IQueryable<TEntity> Select() =>
            _clinicalsMedicsContext.Set<TEntity>();

        public TEntity Select(int id) =>
            _clinicalsMedicsContext.Set<TEntity>().Find(id);

    }
}

