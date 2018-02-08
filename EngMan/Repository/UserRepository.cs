using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EngMan.Models;

namespace EngMan.Repository
{
    public class UserRepository: IUserRepository
    {
        public IEnumerable<User> Users {
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

        public async Task<UserView> SaveUser(UserView user) {
            if(user != null)
            {
                var entity = await context.Users.FindAsync(user.Id);
                if (entity != null)
                {
                    entity.FirstName = user.FirstName;
                    entity.LastName = user.LastName;
                    entity.Email = user.Email;
                }
            }
            context.SaveChanges();
            return user;
        }

        //entity findasync()
        public UserView ChangePassword(int id, string oldpassword, string newpassword)
        {
            var user = new UserView();
            if (!newpassword.Contains(" ") && newpassword.Length > 8 && oldpassword.Length > 0)
            {
                if (id > 0)
                {
                    var entity = context.Users.Find(id);
                    if (entity != null)
                    {
                        entity.Password = oldpassword.Equals(entity.Password) ? newpassword : entity.Password;
                        if (entity.Password.Equals(newpassword))
                        {
                            user = new UserView
                            {
                                Id = entity.Id,
                                FirstName = entity.FirstName,
                                LastName = entity.LastName,
                                Email = entity.Email,
                                Role = entity.Role
                            };
                        }
                    }
                    context.SaveChanges();
                }
            }
            return user;
        }

        public async Task<UserView> ChangeRole(UserView user) {
            if (user != null)
            {
                var entity = await context.Users.FindAsync(user.Id);
                if(entity != null)
                {
                    entity.Role = user.Role;
                }
                context.SaveChanges();
            }
            return user;
        }

        public UserView AddUser(User user) {
            var _user = new UserView();
            if (user != null)
            {
                var entity = context.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (entity == null)
                {
                    if (user.Id == 0)
                    {
                        context.Users.Add(user);
                        context.SaveChanges();
                        user.Id = context.Users.ToArray().Last().Id;
                    }
                    _user = new UserView
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Role = user.Role
                    };
                }
                context.SaveChanges();
            }
            return _user;
        }

        public int DeleteUser(int id) {
            if (id > 0)
            {
                var entity = context.Users.Find(id);
                if (entity != null)
                {
                    context.Users.Remove(entity);
                }
                context.SaveChanges();
                return entity.Id;
            }
            return -1;
        }
    }
}