using Models;

namespace Interfaces {
    public interface IUserRepository : BaseRepository<User>{
        User? GetByEmail(string email);
    }
}
