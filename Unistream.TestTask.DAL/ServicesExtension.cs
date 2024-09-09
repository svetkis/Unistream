using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unistream.TestTask.DAL.Context;
using Unistream.TestTask.DAL.Repositories;

namespace Unistream.TestTask.DAL;

public static class ServicesExtension
{
    public static IServiceCollection ConfigureDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(nameof(TransactionRepository));
        services.Configure<TransactionRepositoryConfig>(section);

        return services;
    }

    public static IServiceCollection RegisterDataContext(this IServiceCollection services)
    {
        services.AddSingleton<ITransactionContext, TransactionContext>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();

        return services;
    }
}
