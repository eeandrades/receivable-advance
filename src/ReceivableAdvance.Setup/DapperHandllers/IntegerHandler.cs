using Dapper;
using System.Data;

namespace ReceivableAdvance.Setup.DapperHandllers;

public class IntegerHandler : SqlMapper.TypeHandler<int?>
{
    public override void SetValue(IDbDataParameter parameter, int? value)
        => parameter.Value = value;

    public override int? Parse(object value)
        => value != null ? Convert.ToInt32(value) : default;
}
