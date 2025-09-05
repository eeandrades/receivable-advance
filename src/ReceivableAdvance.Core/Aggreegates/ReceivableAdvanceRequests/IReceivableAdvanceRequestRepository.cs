namespace ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

public interface IReceivableAdvanceRequestRepository
{
    Task InsertAsync(ReceivableAdvanceRequest request);
    Task<ReceivableAdvanceRequest?> GetByIdAsync(Guid id);
    Task<ReceivableAdvanceRequest?> GetPendingByCreatorAsync(Guid creatorId);
    Task UpdateAsync(ReceivableAdvanceRequest request);
}
