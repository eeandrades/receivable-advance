using Microsoft.AspNetCore.Mvc;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;
using ReceivableAdvance.Application.Commands.FinishReceivableAdvanceRequests;
using ReceivableAdvance.WebApi.Common.Notifications;

namespace ReceivableAdvance.WebApi.Endpoints.ReceivableAdvanceRequests.FinishReceivableAdvanceRequests;

public static class FinishReceivableAdvanceRequestEndpoint
{

    public record Response(RequestStatus NewStatus , DateTime FinishDate)
    {
        public static implicit operator Response(FinishReceivableAdvanceRequestResult result) => new(result.NewStatus, result.FinishDate);
    }

    private static async Task<IResult> ExecuteAsync(IFinishReceivableAdvanceRequestHandler handler, FinishReceivableAdvanceRequestCommand command)
    {
        var result = await handler.ExecuteAsync(command);
        return result.MapMinimalApiResult(value => value != null ? (Response)value : default);
    }

    private static  Task<IResult> Aprove([FromRoute] Guid requestId, [FromServices] IFinishReceivableAdvanceRequestHandler handler) 
        => ExecuteAsync(handler, new ApproveReceivableAdvanceRequestCommand(requestId));

    private static Task<IResult> Reject([FromRoute] Guid requestId, [FromServices] IFinishReceivableAdvanceRequestHandler handler)
        => ExecuteAsync(handler, new RejectReceivableAdvanceRequestCommand(requestId));



    public static RouteGroupBuilder MapFinishReceivableAdvanceRequestEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPatch("/{requestId}/approve", Aprove)
            .Produces<Response>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithOpenApi();

        group
            .MapPatch("/{requestId}/reject", Reject)
            .Produces<Response>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithOpenApi();


        return group;
    }
}
