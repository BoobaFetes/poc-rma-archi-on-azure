using ARO.Risk.Rma.Fopi.Application.Fopi;
using ARO.Risk.Rma.Fopi.Application.Infrastructure;
using ARO.Risk.Rma.Fopi.Infra.Database.Fake;

namespace ARO.Risk.Rma.Fopi.Api
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfraDependencies(this IServiceCollection services)
        {
            services.AddScoped<IInMemoryDatabase, InMemoryDatabase>();

            return services;
        }
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IFopiUsecases, FopiUsecases>();

            return services;
        }
    }
}
