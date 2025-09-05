using Dapper;
using System.Data;

namespace ReceivableAdvance.Setup.DapperHandllers;

public class DateTimeHandler : SqlMapper.TypeHandler<DateTime?>
{
    public override void SetValue(IDbDataParameter parameter, DateTime? value)
        => parameter.Value = value;

    public override DateTime? Parse(object value)
        => value != null ? Convert.ToDateTime(value) : default;
}
