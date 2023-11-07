using Interfaces;
using Models;

namespace Repositories {
    public class UserRepository : IUserRepository {
        private readonly DbConnection _context = new();
        public User Add(User user) {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
        public List<User> GetAll() {
            return _context.Users.ToList();
        }

        public User? GetByEmail(string email) {
            User? user;
            try {
                user = _context.Users.Where(
                    user => user.email == email
                ).First();
            } catch (Exception) {
                user = null;
            };

            return user;
        }

        public User? GetById(int id) {
            User? user;
            try {
                user = _context.Users.Where(
                    user => user.id == id
                ).First();
            } catch (Exception) {
                user = null;
            };

            return user;
        }

        public bool Edit(User user) {
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }
    }
}