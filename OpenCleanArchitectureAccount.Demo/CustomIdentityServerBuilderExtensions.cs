using Microsoft.Extensions.DependencyInjection;
using OpenCleanArchitectureAccount.Abstraction.Interfaces;
using OpenCleanArchitectureAccount.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.Demo
{
    public static class CustomIdentityServerBuilderExtensions
    {
        public static IServiceCollection AddUserRepository(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();

            return services;
        }
    }
}
