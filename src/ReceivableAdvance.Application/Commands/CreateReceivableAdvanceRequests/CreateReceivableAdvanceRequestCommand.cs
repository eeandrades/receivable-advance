﻿namespace ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;

public sealed record CreateReceivableAdvanceRequestCommand(Guid CreatorId, decimal RequestAmount);
