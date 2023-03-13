using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Shine.Backend.Identity
{
    public static class IdentityRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // Provide the default scheme to be used as default
                .AddCookie(o => o.LoginPath = "account/login"); //Adds auth scheme to the configuration

            return services;
        }
    }
}