using Dapper;
using System.Data;

namespace ReceivableAdvance.Setup.DapperHandllers;

public class DecimalHandler : SqlMapper.TypeHandler<decimal?>
{
    public override void SetValue(IDbDataParameter parameter, decimal? value)
        => parameter.Value = value;

    public override decimal? Parse(object value)
        => value != null ? Convert.ToDecimal(value) : default;
}
