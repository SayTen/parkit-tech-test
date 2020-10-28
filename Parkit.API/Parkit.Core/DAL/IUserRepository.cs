using Parkit.Core.Models;
using System;
using System.Threading.Tasks;

namespace Parkit.Core.DAL
{
    public interface IUserRepository
    {
        public void InsertUser(User user);
        public Task<User?> GetUserById(Guid entityId);
        public void DeleteUser(Guid entityId);
        public void UpdateUser(User user);
        public Task Save();
    }
}
