using System.Linq;
using EngMan.Models;
using EngMan.Extensions;

namespace EngMan.Repository
{
    public class UserRepository: IUserRepository
    {
        private EFDbContext context;

        public UserRepository(EFDbContext _context)
        {
            context = _context;
        }
        public IQueryable<User> GetAll()
        {
            return context.Users;
        }

        public User Get(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public bool Add(User user)
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

        public bool Edit(User user)
        {
            if(user == null)
            {
                throw new System.ArgumentNullException();
            }
            var entity = context.Users.Find(user.Id);
            if (entity == null)
            {
                return false;
            }
            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.Email = user.Email;
            context.SaveChanges();
            return true;
        }

        public int Delete(int id)
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

        public bool ChangeRole(UserView user)
        {
            if (user == null)
            {
                throw new System.ArgumentNullException();
            }
            var entity = context.Users.Find(user.Id);
            if (entity == null)
            {
                return false;
            }
            entity.Role = user.Role;
            context.SaveChanges();
            return true;
        }
    }
}