using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Unistream.TestTask.BLL;

public static class ServicesExtension
{
    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
