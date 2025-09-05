using Dapper;

namespace ReceivableAdvance.Infra.Data.ReceivableAdvanceRequests.Mapper.Commands;
public static class InsertReceivableAdvanceRequestCommand
{
    private const string Command = @"
		INSERT INTO receivable_advance_request
			(receivable_advance_request_uid, creator_uuid, request_date, request_amount, net_amount, request_status_id)
        VALUES
			(@ReceivableAdvanceRequestUid, @CreatorUuid, @RequestDate, @RequestAmount, @NetAmount, @RequestStatusId)";


    public record Parameter(
        Guid ReceivableAdvanceRequestUid,
        Guid CreatorUuid,
        DateTime RequestDate,
        decimal RequestAmount,
        decimal NetAmount,
        int RequestStatusId,
        DateTime? FinishDate
    );

    public static Task InsertReceivableAdvanceRequest(this DataContext dtc, Parameter parameters)
    {
        return dtc.Connection.ExecuteAsync(Command, parameters);
    }
}
