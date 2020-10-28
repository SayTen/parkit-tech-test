using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parkit.Core.DAL;
using Parkit.Core.DAL.Contexts;

namespace Parkit.Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddParkitCore(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<UserContext>(options => options.UseSqlite(configuration.GetValue<string>("DbPath")))
                .AddTransient<IUserRepository, UserRepository>();
    }
}
