using Microsoft.AspNetCore.Mvc;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Application.Queries.ListReceivableAdvanceRequests;
using ReceivableAdvance.WebApi.Common;
using ReceivableAdvance.WebApi.Common.Notifications;

namespace ReceivableAdvance.WebApi.Endpoints.ReceivableAdvanceRequests.CreateReceivableAdvanceRequests;

public static class ListReceivableAdvanceRequestEndpoint
{

    public sealed record Response(
        Guid ReceivableAdvanceRequestId,
        decimal RequestAmount,
        decimal NetAmount,
        DateTime RequestDate,
        RequestStatus Status,
        DateTime? FinishDate)
    {
        public static implicit operator Response(ListReceivableAdvanceRequestResult result) => new(
            result.ReceivableAdvanceRequestId,
            result.RequestAmount,
            result.NetAmount,
            result.RequestDate,
            result.Status,
            result.FinishDate);
    }

    public record Envelope(IEnumerable<Response> Items);

    private static async Task<IResult> ExecuteAsync([FromQuery] Guid creatorId, [FromServices] IListReceivableAdvanceRequestHandler handler)
    {
        var result = await handler.ExecuteAsync(new(creatorId));

        return result.MapMinimalApiResult(value => value != null ? new Envelope(value.Select(r => (Response)r)) : default);
    }

    public static RouteGroupBuilder MapListReceivableAdvanceRequestEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapGet("", ExecuteAsync)
            .Produces<Envelope>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        return group;
    }
}
