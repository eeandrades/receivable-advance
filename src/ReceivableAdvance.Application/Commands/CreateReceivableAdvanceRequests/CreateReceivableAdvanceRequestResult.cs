﻿namespace ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;

public sealed record CreateReceivableAdvanceRequestResult(Guid ReceivableAdvanceRequestId, DateTime RequestDate, decimal NetAmmount);
