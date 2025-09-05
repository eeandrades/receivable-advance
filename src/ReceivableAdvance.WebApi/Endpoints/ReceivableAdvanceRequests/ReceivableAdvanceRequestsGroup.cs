using Asp.Versioning.Builder;
using ReceivableAdvance.WebApi.Endpoints.ReceivableAdvanceRequests.CreateReceivableAdvanceRequests;
using ReceivableAdvance.WebApi.Endpoints.ReceivableAdvanceRequests.FinishReceivableAdvanceRequests;
using ReceivableAdvance.WebApi.Endpoints.ReceivableAdvanceRequests.SimulateReceivableAdvanceRequests;

namespace ReceivableAdvance.WebApi.Endpoints.ReceivableAdvanceRequests;

public static class ReceivableAdvanceRequestsGroup
{
    public static IEndpointRouteBuilder MapReceivableAdvanceRequestsGroup(this IEndpointRouteBuilder routes, ApiVersionSet versions)
    {
        routes.MapGroup("v{version:apiVersion}/receivable-advance-requests")
            .WithTags("ReceivableAdvanceRequests")
            .WithApiVersionSet(versions)
            .HasApiVersion(new(1, 0))
            .MapCreateReceivableAdvanceRequestEndpoint()
            .MapFinishReceivableAdvanceRequestEndpoint()
            .MapSimulateReceivableAdvanceRequestEndpoint()
            .MapListReceivableAdvanceRequestEndpoint();
        return routes;
    }
}
