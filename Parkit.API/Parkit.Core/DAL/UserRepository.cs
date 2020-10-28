using Parkit.Core.DAL.Contexts;
using Parkit.Core.DAL.Contexts.DataMappers;
using Parkit.Core.Models;
using System;
using System.Threading.Tasks;

namespace Parkit.Core.DAL
{
    internal class UserRepository : IUserRepository, IDisposable
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public void InsertUser(User user)
        {
            user.Created = DateTime.UtcNow;
            var dbUser = UserDataMapper.MapToDb(user);
            _userContext.Users.Add(dbUser);
            user.Id = dbUser.Id;
        }

        public void DeleteUser(Guid entityId)
        {
            var user = _userContext.Users.Find(entityId);
            _userContext.Users.Remove(user);
        }

        public async Task<User?> GetUserById(Guid entityId)
        {
            var user = await _userContext.Users.FindAsync(entityId);

            if (user is null)
            {
                return null;
            }

            return UserDataMapper.MapFromDb(user);
        }

        public void UpdateUser(User user)
        {
            var trackedUser = _userContext.Users.Find(user.Id);

            if (trackedUser is null)
            {
                return;
            }

            trackedUser.Email = user.Email;
            trackedUser.FamilyName = user.FamilyName;
            trackedUser.GivenName = user.GivenName;
        }

        public async Task Save()
        {
            await _userContext.SaveChangesAsync();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _userContext.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
