using Identity.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<ContextPostgreSQL>(options =>
                options.UseNpgsql(configuration.GetConnectionString("conexion")));

            services.AddAuthorization();

            services.AddIdentityApiEndpoints<IdentityUser>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            })
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<ContextPostgreSQL>();

            //services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
