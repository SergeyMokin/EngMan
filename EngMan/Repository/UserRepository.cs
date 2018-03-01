﻿using System.Collections.Generic;
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
            if (id > 0)
            {
                var entity = context.Users.Find(id);
                if (entity != null)
                {
                    if (entity.Password.VerifyHashedPassword(oldpassword))
                    {
                        entity.Password = newpassword.HashPassword();
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> ChangeRole(UserView user)
        {
            if (user == null)
            {
                throw new System.ArgumentNullException();
            }
            var entity = await context.Users.FindAsync(user.Id);
            if (entity != null)
            {
                entity.Role = user.Role;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddUser(User user)
        {
            if (user == null)
            {
                throw new System.ArgumentNullException();
            }
            var entity = context.Users.Where(x => x.Email == user.Email).FirstOrDefault();
            if (entity == null)
            {
                User added = new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName.Substring(0, 1).ToUpper() + user.FirstName.Remove(0, 1),
                    LastName = user.LastName.Substring(0, 1).ToUpper() + user.LastName.Remove(0, 1),
                    Email = user.Email.ToLower(),
                    Password = Extensions.Extensions.HashPassword(user.Password),
                    Role = user.Role
                };
                if (added.Id == 0)
                {
                    context.Users.Add(added);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public int DeleteUser(int id)
        {
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