using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Commands.SimulateReceivableAdvanceRequests;

public record ReceivableAdvanceRequestSimulatedSuccess() :
    Notification("Advance request simulated successfully", Levels.Success);

public record InvalidRequestAmount(decimal amount) :
    Notification($"The requested amount must be greater than zero. {amount}", Levels.BusinessError);



