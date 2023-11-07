namespace Interfaces {
    public interface IBaseRepository<T> {
        T Add (T entity);
        List<T> GetAll();
        T? GetById(int id);
        bool Edit(T entity);
    }
}