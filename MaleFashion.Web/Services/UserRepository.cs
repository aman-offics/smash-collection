using System.Collections.Generic;
using System.Linq;
using MaleFashion.Web.Models;

namespace MaleFashion.Web.Services
{
    public class UserRepository
    {
        private readonly Data.AppDbContext _context;

        public UserRepository(Data.AppDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
        }

        public bool ValidateUser(string username, string password)
        {
            var user = GetUserByUsername(username);
            if (user != null && user.Password == password)
            {
                if (!user.IsActive) return false;
                return true;
            }
            return false;
        }

        public IEnumerable<User> GetAll() => _context.Users.ToList();

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
