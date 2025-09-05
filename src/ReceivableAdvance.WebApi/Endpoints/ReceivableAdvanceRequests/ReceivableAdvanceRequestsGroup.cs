using Asp.Versioning.Builder;
using ReceivableAdvance.WebApi.Endpoints.ReceivableAdvanceRequests.CreateReceivableAdvanceRequests;

namespace ReceivableAdvance.WebApi.Endpoints.ReceivableAdvanceRequests;

public static class ReceivableAdvanceRequestsGroup
{
    public static IEndpointRouteBuilder MapReceivableAdvanceRequestsGroup(this IEndpointRouteBuilder routes, ApiVersionSet versions)
    {
        routes.MapGroup("v{version:apiVersion}/receivable-advance-requests")
            .WithTags("ReceivableAdvanceRequests")
            .WithApiVersionSet(versions)
            .HasApiVersion(new(1, 0))
            .MapCreateReceivableAdvanceRequestEndpoint();



        //group.MapGet("/", GetAllReceivableAdvanceRequestsEndpoint.Handle);
        //group.MapGet("/{id:guid}", GetReceivableAdvanceRequestByIdEndpoint.Handle);
        //group.MapPost("/", CreateReceivableAdvanceRequestEndpoint.Handle);
        //group.MapPut("/{id:guid}", UpdateReceivableAdvanceRequestEndpoint.Handle);
        //group.MapDelete("/{id:guid}", DeleteReceivableAdvanceRequestEndpoint.Handle);
        return routes;
    }
}
