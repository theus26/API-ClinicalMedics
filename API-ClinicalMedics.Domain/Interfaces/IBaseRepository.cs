namespace API_ClinicalMedics.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        void Insert(T obj);

        void Update(T obj);

        void Delete(int id);

        IQueryable<T> Select();

        T Select(int id);
    }
}
