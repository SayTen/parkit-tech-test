namespace Parkit.Core.DAL.Contexts.DataMappers
{
    internal static class UserDataMapper
    {
        public static Models.User MapToDb(Core.Models.User user)
            => new Models.User()
            {
                Id = user.Id,
                Created = user.Created,
                Email = user.Email,
                FamilyName = user.FamilyName,
                GivenName = user.GivenName,
            };

        public static Core.Models.User MapFromDb(Models.User user)
            => new Core.Models.User()
            {
                Id = user.Id,
                Created = user.Created,
                Email = user.Email,
                FamilyName = user.FamilyName,
                GivenName = user.GivenName,
            };
    }
}
