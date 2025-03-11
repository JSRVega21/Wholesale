using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
    
using Wholesale.Models;
using Wholesale.Server.Data;
using Wholesale.Server.Repository;

namespace Wholesale.Server.Repository
{
    public class UserRepository : IUserRepository<User, int>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;
        public UserRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public User Add(User entity)
        {
            var db = _factory.CreateDbContext();
            entity.UserPassword = BCrypt.Net.BCrypt.HashPassword(entity.UserPassword);
            entity.Initialize();
            db.UserWholesale.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public async Task<User> AddAsync(User entity)
        {
            var db = _factory.CreateDbContext();
            entity.UserPassword = BCrypt.Net.BCrypt.HashPassword(entity.UserPassword);
            entity.Initialize();
            db.UserWholesale.Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public void Delete(int key)
        {
            var db = _factory.CreateDbContext();
            User entity = db.UserWholesale.Find(key);
            db.UserWholesale.Remove(entity);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int key)
        {
            var db = _factory.CreateDbContext();
            User entity = await db.UserWholesale.FindAsync(key);
            db.UserWholesale.Remove(entity);
            await db.SaveChangesAsync();
        }

        public User GetByKey(int key)
        {
            return GetByKey(key, true);
        }

        public User GetByKey(int key, bool tracking = false)
        {
            var db = _factory.CreateDbContext();
            return tracking
                ? db.UserWholesale.Find(key)
                : db.UserWholesale.AsNoTracking().FirstOrDefault(u => u.UserId == key);
        }

        public async Task<User> GetByKeyAsync(int key)
        {
            return await GetByKeyAsync(key, true);
        }

        public async Task<User> GetByKeyAsync(int key, bool tracking = false)
        {
            var db = _factory.CreateDbContext();
            if (tracking)
            {
                return await db.UserWholesale.FindAsync(key);
            }
            else
            {
                return await db.UserWholesale.AsNoTracking().FirstOrDefaultAsync(item => item.UserId == key);
            }
        }

        public IList<User> GetList()
        {
            var db = _factory.CreateDbContext();
            return db.UserWholesale.ToList();
        }

        public async Task<IList<User>> GetListAsync()
        {
            var db = _factory.CreateDbContext();
            return await db.UserWholesale.ToListAsync();
        }

        public User Update(User entity)
        {
            var db = _factory.CreateDbContext();
            entity.UserPassword = BCrypt.Net.BCrypt.HashPassword(entity.UserPassword);
            entity.Updated();
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return entity;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            var db = _factory.CreateDbContext();
            var existingUser = await db.UserWholesale.FindAsync(entity.UserId);

            if (existingUser == null)
                throw new Exception("Usuario no encontrado");

            existingUser.UserName = entity.UserName;
            existingUser.SlpCode = entity.SlpCode;
            existingUser.U_CodigoPOS = entity.U_CodigoPOS;
            existingUser.SlpName = entity.SlpName;
            existingUser.UserEmail = entity.UserEmail;
            existingUser.UserPhone = entity.UserPhone;
            existingUser.UserRoleId = entity.UserRoleId;
            existingUser.UserRole = entity.UserRole;

            // ❌ Ya no toques la contraseña aquí
            existingUser.UserPassword = entity.UserPassword;

            existingUser.Updated();

            await db.SaveChangesAsync();
            return existingUser;
        }





        public ApplicationDbContext GetDbContext()
        {
            return _factory.CreateDbContext();
        }

    }
}
