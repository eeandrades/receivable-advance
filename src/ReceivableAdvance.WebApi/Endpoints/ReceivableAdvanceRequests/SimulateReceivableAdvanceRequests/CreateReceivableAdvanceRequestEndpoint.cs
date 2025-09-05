using Microsoft.AspNetCore.Mvc;
using ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;
using ReceivableAdvance.Application.Commands.SimulateReceivableAdvanceRequests;
using ReceivableAdvance.WebApi.Common.Notifications;

namespace ReceivableAdvance.WebApi.Endpoints.ReceivableAdvanceRequests.SimulateReceivableAdvanceRequests;

public static class SimulateReceivableAdvanceRequestEndpoint
{
    public record Request(Guid CreatorId, decimal RequestAmount)
    {
        public static implicit operator SimulateReceivableAdvanceRequestCommand(Request request) => new(request.CreatorId, request.RequestAmount);
    }

    public record Response(DateTime RequestDate, decimal NetAmmount)
    {
        public static implicit operator Response(SimulateReceivableAdvanceRequestResult result) => new(result.RequestDate, result.NetAmmount);
    }

    private static async Task<IResult> ExecuteAsync([FromBody] Request request, [FromServices] ISimulateReceivableAdvanceRequestHandler handler)
    {
        var result = await handler.ExecuteAsync(request);
        return result.MapMinimalApiResult(value => value != null ? (Response)value : default);
    }

    public static RouteGroupBuilder MapSimulateReceivableAdvanceRequestEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("simulations", ExecuteAsync)
            .Produces<Response>(StatusCodes.Status200OK)
            .WithOpenApi();
        return group;
    }
}
