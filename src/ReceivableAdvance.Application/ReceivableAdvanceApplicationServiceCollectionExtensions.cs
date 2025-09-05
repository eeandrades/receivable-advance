using Microsoft.Extensions.DependencyInjection;
using ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;
using ReceivableAdvance.Application.Commands.FinishReceivableAdvanceRequests;
using ReceivableAdvance.Application.Commands.SimulateReceivableAdvanceRequests;
using ReceivableAdvance.Application.Queries.ListReceivableAdvanceRequests;

namespace ReceivableAdvance.Application;

public static class ReceivableAdvanceApplicationServiceCollectionExtensions
{
    public static IServiceCollection SetupApplication(this IServiceCollection services) => services
        .AddScoped<ICreateReceivableAdvanceRequestHandler, CreateReceivableAdvanceRequestHandler>()
        .AddScoped<IFinishReceivableAdvanceRequestHandler, FinishReceivableAdvanceRequestHandler>()
        .AddScoped<ISimulateReceivableAdvanceRequestHandler, SimulateReceivableAdvanceRequestHandler>()
        .AddScoped<IListReceivableAdvanceRequestHandler, ListReceivableAdvanceRequestHandler>();
}
