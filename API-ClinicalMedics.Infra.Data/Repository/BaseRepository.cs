using API_ClinicalMedics.Domain.Interfaces;
using API_ClinicalMedics.Infra.Data.Context;

namespace API_ClinicalMedics.Infra.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public ClinicalsMedicsContext _context { get; set; }

        public BaseRepository()
        {
            
               _context = new ClinicalsMedicsContext();
            
        }
        public void Delete(int id)
        {
            _context.Set<T>().Remove(Select(id));
            _context.SaveChanges();
        }

        public void Insert(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
        }

        public IList<T> Select() =>
            _context.Set<T>().ToList();

        public T Select(int id) =>
            _context.Set<T>().Find(id);

        public void Update(T obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
