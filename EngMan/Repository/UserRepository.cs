using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EngMan.Models;
using EngMan.Extensions;
namespace EngMan.Repository
{
    public class UserRepository: IUserRepository
    {
        public IEnumerable<User> Users
        {
            get
            {
                return context.Users;
            }
        }

        private EFDbContext context;

        public UserRepository(EFDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> SaveUser(UserView user)
        {
            if(user == null)
            {
                throw new System.ArgumentNullException();
            }
            var entity = await context.Users.FindAsync(user.Id);
            if (entity != null)
            {
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.Email = user.Email;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        
        public bool ChangePassword(int id, string oldpassword, string newpassword)
        {
            if (oldpassword == null || newpassword == null)
            {
                throw new System.ArgumentNullException();
            }
            if (id < 1)
            {
                return false;
            }
            var entity = context.Users.Find(id);
            if (entity == null || !entity.Password.VerifyHashedPassword(oldpassword))
            {
                return false;
            }
            entity.Password = newpassword.HashPassword();
            context.SaveChanges();
            return true;
        }

        public async Task<bool> ChangeRole(UserView user)
        {
            if (user == null)
            {
                throw new System.ArgumentNullException();
            }
            var entity = await context.Users.FindAsync(user.Id);
            if (entity == null)
            {
                return false;
            }
            entity.Role = user.Role;
            context.SaveChanges();
            return true;
        }

        public bool AddUser(User user)
        {
            if (user == null)
            {
                throw new System.ArgumentNullException();
            }
            var entity = context.Users.Where(x => x.Email == user.Email).FirstOrDefault();
            if (entity != null || user.Id != 0)
            {
                return false;
            }
            User added = new User
            {
                Id = user.Id,
                FirstName = user.FirstName.Substring(0, 1).ToUpper() + user.FirstName.Remove(0, 1),
                LastName = user.LastName.Substring(0, 1).ToUpper() + user.LastName.Remove(0, 1),
                Email = user.Email.ToLower(),
                Password = Extensions.Extensions.HashPassword(user.Password),
                Role = user.Role
            };
            context.Users.Add(added);
            context.SaveChanges();
            return true;
        }

        public int DeleteUser(int id)
        {
            if (id < 1)
            {
                return -1;
            }
            var entity = context.Users.Find(id);
            if (entity == null)
            {
                return -1;
            }
            context.Users.Remove(entity);
            context.SaveChanges();
            return entity.Id;
        }
    }
}