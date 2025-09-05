using Dapper;
using System.Data;

namespace ReceivableAdvance.Setup.DapperHandllers;

public class GuidHandler : SqlMapper.TypeHandler<Guid?>
{
    public override void SetValue(IDbDataParameter parameter, Guid? value)
        => parameter.Value = value;

    public override Guid? Parse(object value)
    {
        string? strValue = Convert.ToString(value);

        return strValue != null ? Guid.Parse(strValue) : default;

    }
}
