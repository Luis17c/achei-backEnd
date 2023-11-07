using Models;

namespace Interfaces {
    public interface IUserRepository {
        User Add(User user);
        List<User> GetAll();

        User? GetByEmail(string email);

        User? GetById(int id);
        
        bool Edit(User user);
    }
    
}
