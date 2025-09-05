using Dapper;

namespace ReceivableAdvance.Setup.DapperHandllers;
public static class SqlMapperHelper
{
    public static void MapTypesForSqlite()
    {
        SqlMapper.AddTypeHandler(new DateTimeHandler());
        SqlMapper.AddTypeHandler(new GuidHandler());
        SqlMapper.AddTypeHandler(new IntegerHandler());
        SqlMapper.AddTypeHandler(new DecimalHandler());
    }
}
