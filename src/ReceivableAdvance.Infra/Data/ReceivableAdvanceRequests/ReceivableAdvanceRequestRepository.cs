using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Infra.Data.ReceivableAdvanceRequests.Mapper.Commands;
using ReceivableAdvance.Infra.Data.ReceivableAdvanceRequests.Mapper.Queries;
using System.Transactions;

namespace ReceivableAdvance.Infra.Data.ReceivableAdvanceRequests;

public class ReceivableAdvanceRequestRepository(DataContext dataContext) : IReceivableAdvanceRequestRepository
{
    public async Task<ReceivableAdvanceRequest?> GetByIdAsync(Guid id)
    {
        var resultSet = await dataContext.GetReceivableAdvanceRequestById(id);
        return resultSet != null ? CreateEntity(resultSet) : default;
    }

    public async Task<ReceivableAdvanceRequest?> GetPendingByCreatorAsync(Guid creatorId)
    {
        var resultSet = await dataContext.GetPendingReceivableAdvanceRequestByCreatorId(creatorId);


        return resultSet != null? CreateEntity(resultSet): default;
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

    public Task UpdateAsync(ReceivableAdvanceRequest request)
    {
        return dataContext.UpdateReceivableAdvanceRequest(new(request.Id, (int)request.Status, request.FinishDate!.Value));
    }

    public async Task<IEnumerable<ReceivableAdvanceRequest>> ListByCreatorIdAsync(Guid creatorId)
    {
        var dbList = await dataContext.ListReceivableAdvanceRequestByCreatorId(creatorId);

        return dbList.Select(CreateEntity);
    }

    private static ReceivableAdvanceRequest CreateEntity(GetReceivableAdvanceRequestQuery.ResultSet resultSet)
    {
        return new ReceivableAdvanceRequest(
            resultSet.ReceivableAdvanceRequestUid,
            resultSet.CreatorUuid,
            Convert.ToDecimal(resultSet.RequestAmount),
            Convert.ToDecimal(resultSet.NetAmount),
            resultSet.RequestDate,
            (RequestStatus)resultSet.RequestStatusId, 
            resultSet.FinishDate);
    }

}
