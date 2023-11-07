namespace Interfaces {
    public interface BaseRepository<T> {
        T Add (T entity);
        List<T> GetAll();
        T? GetById(int id);
        bool Edit(T entity);
    }
}