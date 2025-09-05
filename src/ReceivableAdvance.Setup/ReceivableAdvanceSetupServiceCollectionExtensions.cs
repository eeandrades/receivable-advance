using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using ReceivableAdvance.Application;
using ReceivableAdvance.Infra.Data;
using ReceivableAdvance.Setup.DapperHandllers;
using System.Data.Common;

namespace ReceivableAdvance.Setup;

public static class ReceivableAdvanceSetupServiceCollectionExtensions
{
    public static IServiceCollection SetupReceivableAdvance(this IServiceCollection services) => services
        .SetupCoreDomain()
        .SetupSqlite()
        .SetupData()
        .SetupApplication();


    private static IServiceCollection SetupSqlite(this IServiceCollection services)
    {
        //injetando a dependencia do sqlite aqui, minha camada a infra fica agnostica ao mecanimos de banco de dados, 
        //desta forma no futuro posso trocar mais facilmente o banco além disso com essa estratégia posso fazer testes unitários 
        //nos repositórios utilizando, por exemplo, um banco de dados em memória.

        services.AddSingleton<DbProviderFactory>(SqliteFactory.Instance);


        services.AddSingleton(new DbConnectionStringBuilder()
        {
            ["Data Source"] = GetDbPath()
        });

        //O sqllite tem um número de tipos limitados e o Dapper não os mapeia corretamente,
        //para isso é preciso relizar um mapeamento personalizado para cada tipo utilizado no projeto 
        //(Guid, Datetime, Decimal e int)
        SqlMapperHelper.MapTypesForSqlite();

        return services;

    }

    private static string GetDbPath()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "receivable-advance-db.sqlite");
    }
}
