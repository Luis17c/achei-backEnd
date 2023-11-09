using Models;

namespace Interfaces {
    public interface IBaseRepository<T> {
        T Add (T entity);
        List<T> GetAll(Func<T, bool>? query);
        T? GetById(int id);
        bool Edit(T entity);
    }
}