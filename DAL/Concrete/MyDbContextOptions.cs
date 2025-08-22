using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Concrete
{
    public static class MyDbContextOptions
    {
        public static void AddMainDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<MainDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        }
    }
}