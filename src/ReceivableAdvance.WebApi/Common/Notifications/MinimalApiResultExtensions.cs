using Microsoft.AspNetCore.Mvc;
using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.WebApi.Common.Notifications;

public static class MinimalApiResultExtensions
{
    private readonly static Dictionary<Level, Func<object?, IResult>> ResultMapper = new()
    {
        {Levels.Success, (o) => Results.Ok(o) },
        {Levels.Created, (o) => Results.Created(string.Empty, o) },
        {Levels.BusinessError, (o) => Results.UnprocessableEntity(o) },
        {Levels.NotFound, (o) => Results.NotFound(o) },
    };


    public static IResult MapMinimalApiResult<TValue>(this Result<TValue> result, Func<TValue?, object?> getResponse)
    {
        var response = getResponse.Invoke(result.Value);

        var notification = result.Notification;

        if (response == null && !result.IsValid)
        {
            response = new ProblemDetails()
            {
                Detail = notification.Message,
                Title = notification.Level.Name,
                Type = notification.GetType().Name,
            };
        }

        return ResultMapper[result.Notification.Level].Invoke(response);
    }
}
