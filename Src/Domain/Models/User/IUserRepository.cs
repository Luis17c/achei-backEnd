using Models;

namespace Interfaces {
    public interface IUserRepository : IBaseRepository<User>{
        User? GetByEmail(string email);
    }
}
