using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Common.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceivableAdvance.Application.Commands.FinishReceivableAdvanceRequests;
public abstract record FinishReceivableAdvanceRequestCommand(Guid RequestId)
{
    public abstract Notification Finish(ReceivableAdvanceRequest request, DateTime finishDate);
}
