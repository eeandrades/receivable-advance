using Dapper;

namespace ReceivableAdvance.Infra.Data.ReceivableAdvanceRequests.Mapper.Commands;
public static class UpdateReceivableAdvanceRequestCommand
{
    private const string Command = @"
        UPDATE
	        receivable_advance_request
        SET
	        request_status_id = @RequestStatusId,
	        finish_date = @FinishDate
        WHERE
	        receivable_advance_request_uid = @ReceivableAdvanceRequestUid";

    public record Parameter(
        Guid ReceivableAdvanceRequestUid,
        int RequestStatusId,
        DateTime FinishDate);

    public static Task UpdateReceivableAdvanceRequest(this DataContext dtc, Parameter parameters)
    {
        return dtc.Connection.ExecuteAsync(Command, parameters);
    }
}
