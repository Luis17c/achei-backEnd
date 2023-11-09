using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories {
    public class UserRepository : IUserRepository {
        private readonly DbConnection _context = new();
        public User Add(User user) {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
        public List<User> GetAll(Func<User, bool> ?query) {
            var users = _context.Users
                .Where(
                    u => u.isActive == true
                ).Include(
                    u => u.address
                ).Include(
                    u => u.companies
                );

            if (query != null) {
                return users.Where(query).ToList();
            }
            
            return users.ToList();
        }

        public User? GetByEmail(string email) {
            User? user;
            try {
                user = _context.Users
                .Where(
                    user => user.email == email
                ).Where(
                    u => u.isActive == true
                ).Include(
                    u => u.address
                ).Include(
                    u => u.companies
                ).First();
            } catch (Exception err) {
                Console.WriteLine(err);
                user = null;
            };

            return user;
        }

        public User? GetById(int id) {
            User? user;
            try {
                user = _context.Users
                    .Where(
                        user => user.id == id
                    ).Where(
                        u => u.isActive == true
                    ).Include(
                        u => u.address
                    ).Include(
                        u => u.companies
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