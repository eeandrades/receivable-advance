using Microsoft.Extensions.DependencyInjection;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Infra.Data.ReceivableAdvanceRequests;

namespace ReceivableAdvance.Infra.Data;

public static class ReceivableAdvanceDataServiceCollectionExtensions
{
    public static IServiceCollection SetupData(this IServiceCollection services) => services
        .AddScoped<IReceivableAdvanceRequestRepository, ReceivableAdvanceRequestRepository>()
        .AddScoped<DataContext>();
}
