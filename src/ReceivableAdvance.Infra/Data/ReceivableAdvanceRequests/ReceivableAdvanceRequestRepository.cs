using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Infra.Data.ReceivableAdvanceRequests.Mapper.Commands;
using ReceivableAdvance.Infra.Data.ReceivableAdvanceRequests.Mapper.Queries;

namespace ReceivableAdvance.Infra.Data.ReceivableAdvanceRequests;

public class ReceivableAdvanceRequestRepository(DataContext dataContext) : IReceivableAdvanceRequestRepository
{
    public async Task<ReceivableAdvanceRequest?> GetByIdAsync(Guid id)
    {
        var resultSet = await dataContext.GetReceivableAdvanceRequestById(id);

        return CreateEntity(resultSet);
    }

    public async Task<ReceivableAdvanceRequest?> GetPendingByCreatorAsync(Guid creatorId)
    {
        var resultSet = await dataContext.GetPendingReceivableAdvanceRequestByCreatorId(creatorId);

        return CreateEntity(resultSet);
    }

    public Task InsertAsync(ReceivableAdvanceRequest request)
    {
        return dataContext.InsertReceivableAdvanceRequest(new(
            request.Id,
            request.CreatorId,
            request.RequestDate,
            request.RequestValue,
            request.NetValue,
            (int)request.Status,
            request.RequestDate));
    }

    private static ReceivableAdvanceRequest? CreateEntity(GetReceivableAdvanceRequestQuery.ResultSet? resultSet)
    {
        if (resultSet is null)
            return default;

        return new ReceivableAdvanceRequest(
            resultSet.ReceivableAdvanceRequestUid,
            resultSet.CreatorUuid,
            Convert.ToDecimal(resultSet.RequestAmount),
            Convert.ToDecimal(resultSet.NetAmount),
            resultSet.RequestDate,
            (RequestStatus)resultSet.RequestStatusId);
    }
}
