using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Commands.SimulateReceivableAdvanceRequests;

public record ReceivableAdvanceRequestSimulatedSuccess() :
    Notification("Advance request simulated successfully", Levels.Success);




