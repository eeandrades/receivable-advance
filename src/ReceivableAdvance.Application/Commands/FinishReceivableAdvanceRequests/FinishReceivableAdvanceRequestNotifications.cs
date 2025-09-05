using ReceivableAdvance.Common.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceivableAdvance.Application.Commands.FinishReceivableAdvanceRequests;

public record ReceivableAdvanceRequestNotFound(Guid ReceivableAdvanceRequestId) : Notification($"Receivable advance request '{ReceivableAdvanceRequestId}' not found.", Levels.NotFound);
