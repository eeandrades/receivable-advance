using Microsoft.AspNetCore.Mvc;
using ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;
using ReceivableAdvance.WebApi.Common.Notifications;

namespace ReceivableAdvance.WebApi.Endpoints.ReceivableAdvanceRequests.CreateReceivableAdvanceRequests;

public static class CreateReceivableAdvanceRequestEndpoint
{
    public record Request(Guid CreatorId, decimal RequestAmount)
    {
        public static implicit operator CreateReceivableAdvanceRequestCommand(Request request) => new(request.CreatorId, request.RequestAmount);
    }

    public record Response(Guid ReceivableAdvanceRequestId, DateTime RequestDate, Decimal NetAmmount)
    {
        public static implicit operator Response(CreateReceivableAdvanceRequestResult result) => new(result.ReceivableAdvanceRequestId, result.RequestDate, result.NetAmmount);
    }

    private static async Task<IResult> ExecuteAsync([FromBody] Request request, [FromServices] ICreateReceivableAdvanceRequestHandler handler)
    {
        var result = await handler.ExecuteAsync(request);
        return result.MapMinimalApiResult(value => value != null ? (Response)value : default);
    }

    public static RouteGroupBuilder MapCreateReceivableAdvanceRequestEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("/advance-requests", ExecuteAsync)
            .Produces<Response>(StatusCodes.Status201Created)
            .WithOpenApi();
        return group;
    }
}
