using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Payment.Infrastructure.Repositories;

namespace Payment.Infrastructure.ServiceRegistrationExtensions
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services
                .RegisterServices();

            return services;
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            return services;
        }
    }
}
