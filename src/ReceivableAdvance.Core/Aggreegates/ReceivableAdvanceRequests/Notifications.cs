using ReceivableAdvance.Common.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

public record RequestAlreadyFinished(Guid RequestId) : Notification($"Request {RequestId} is already finished.", Levels.BusinessError);

public record RequestFinishedSuccess(Guid RequestId, RequestStatus NewStatus) : Notification($"Request {RequestId} is finished with status {NewStatus}.", Levels.Success);
