using Dapper;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

namespace ReceivableAdvance.Infra.Data.ReceivableAdvanceRequests.Mapper.Queries;
public static class GetReceivableAdvanceRequestQuery
{
    private const string Query = @$"
		SELECT 
        rar.receivable_advance_request_uid {nameof(ResultSet.ReceivableAdvanceRequestUid)}, 
        rar.creator_uuid {nameof(ResultSet.CreatorUuid)},
        rar.request_date {nameof(ResultSet.RequestDate)},
        rar.request_amount * 1.0 {nameof(ResultSet.RequestAmount)},
        rar.net_amount * 1.0 {nameof(ResultSet.NetAmount)},
        rar.request_status_id {nameof(ResultSet.RequestStatusId)}, 
        rar.finish_date {nameof(ResultSet.FinishDate)}
		FROM 
			receivable_advance_request rar
		WHERE
			{{0}}";
    private const string GetReceivableAdvanceRequestByIdFilter = "rar.receivable_advance_request_uid = @ReceivableAdvanceRequestUid";

    private const string GetPendingReceivableAdvanceRequestByCreatorIdFilter = "rar.creator_uuid = @CreatorUuid and rar.request_status_id = @RequestStatusId";

    private const string ListReceivableAdvanceRequestByCreatorIdFilter = "rar.creator_uuid = @CreatorUuid";


    public sealed record ResultSet(
        Guid ReceivableAdvanceRequestUid,
        Guid CreatorUuid,
        DateTime RequestDate,
        double RequestAmount,
        double NetAmount,
        int RequestStatusId,
        DateTime? FinishDate
    );

    private static Task<ResultSet?> GetReceivableAdvanceRequestByFilter(this DataContext dtc, string filter, object paramters)
    {
        var query = string.Format(Query, filter);

        return dtc.Connection.QueryFirstOrDefaultAsync<ResultSet>(query, paramters);
    }

    public static Task<IEnumerable<ResultSet>> ListReceivableAdvanceRequestByCreatorId(this DataContext dtc, Guid creatorId)
    {
        var query = string.Format(Query, ListReceivableAdvanceRequestByCreatorIdFilter);

        var param = new
        {
            CreatorUuid = creatorId,
        };

        return dtc.Connection.QueryAsync<ResultSet>(query, param);
    }


    public static Task<ResultSet?> GetReceivableAdvanceRequestById(this DataContext dtc, Guid receivableAdvanceRequestId)
    {
        var param = new
        {
            ReceivableAdvanceRequestUid = receivableAdvanceRequestId
        };
        return dtc.GetReceivableAdvanceRequestByFilter(GetReceivableAdvanceRequestByIdFilter, param);
    }

    public static Task<ResultSet?> GetPendingReceivableAdvanceRequestByCreatorId(this DataContext dtc, Guid creatorId)
    {
        var param = new
        {
            CreatorUuid = creatorId,
            RequestStatusId = (int)RequestStatus.Pending
        };
        return dtc.GetReceivableAdvanceRequestByFilter(GetPendingReceivableAdvanceRequestByCreatorIdFilter, param);
    }
}
