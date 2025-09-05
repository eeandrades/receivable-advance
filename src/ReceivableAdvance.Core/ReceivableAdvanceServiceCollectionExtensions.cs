using Microsoft.Extensions.DependencyInjection;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

namespace ReceivableAdvance;

public static class ReceivableAdvanceServiceCollectionExtensions
{
    public static IServiceCollection SetupCoreDomain(this IServiceCollection services) => services
        .AddSingleton<IReceivableAdvanceFeePolicy, StandardReceivableAdvanceFeePolicy>();
}
