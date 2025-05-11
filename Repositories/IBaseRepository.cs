namespace ClinicManagementSystem.Repositories
{
    public interface IBaseRepository<T> where T: class
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<T> Create(T entity);
        Task<bool> Update(int id, T entity);
    }
}
