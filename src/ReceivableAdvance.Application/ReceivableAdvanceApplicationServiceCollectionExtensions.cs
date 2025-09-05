using Microsoft.Extensions.DependencyInjection;
using ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;

namespace ReceivableAdvance.Application;

public static class ReceivableAdvanceApplicationServiceCollectionExtensions
{
    public static IServiceCollection SetupApplication(this IServiceCollection services) => services
        .AddScoped<ICreateReceivableAdvanceRequestHandler, CreateReceivableAdvanceRequestHandler>();
}
