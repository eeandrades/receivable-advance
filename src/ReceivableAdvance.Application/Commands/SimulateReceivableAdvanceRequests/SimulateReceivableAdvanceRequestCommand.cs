﻿namespace ReceivableAdvance.Application.Commands.SimulateReceivableAdvanceRequests;

public sealed record SimulateReceivableAdvanceRequestCommand(Guid CreatorId, decimal RequestAmount);
