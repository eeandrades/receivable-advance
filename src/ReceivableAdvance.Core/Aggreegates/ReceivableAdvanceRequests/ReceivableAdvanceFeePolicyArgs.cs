namespace ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

public record ReceivableAdvanceFeePolicyArgs(Guid CreatorId, Money RequestValue, DateTime RequestDate);
