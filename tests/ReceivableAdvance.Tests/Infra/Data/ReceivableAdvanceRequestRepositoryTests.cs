using AutoFixture;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Infra.Data.ReceivableAdvanceRequests;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Xunit;

namespace ReceivableAdvance.Infra.Data.Tests.ReceivableAdvanceRequests;

public class ReceivableAdvanceRequestRepositoryTests
{
    private Fixture _fixture = new();
    private DataContext CreateInMemoryContext()
    {
        var connection = InMemoryDatabaseFactory.CreateAndInitialize();
        ReceivableAdvance.Setup.DapperHandllers.SqlMapperHelper.MapTypesForSqlite();

        return new DataContext(connection, SqliteFactory.Instance, new DbConnectionStringBuilder());
    }

    [Fact]
    public async Task InsertAndGetByIdAsync_ShouldReturnInsertedEntity()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new ReceivableAdvanceRequestRepository(context);
        var request = new ReceivableAdvanceRequest(
            Guid.NewGuid(),
            Guid.NewGuid(),
            1000m,
            950m,
            DateTime.UtcNow,
            RequestStatus.Pending,
            null);

        // Act
        await repository.InsertAsync(request);
        var result = await repository.GetByIdAsync(request.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(request.Id);
        result.CreatorId.Should().Be(request.CreatorId);
        result.RequestValue.Should().Be(request.RequestValue);
        result.NetValue.Should().Be(request.NetValue);
        result.Status.Should().Be(request.Status);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateStatusAndFinishData_WhenApproveReques()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new ReceivableAdvanceRequestRepository(context);
        var request = new ReceivableAdvanceRequest(
            Guid.NewGuid(),
            Guid.NewGuid(),
            1000m,
            950m,
            DateTime.UtcNow,
            RequestStatus.Pending,
            null);

        await repository.InsertAsync(request);
        // Act

        request.Approve(DateTime.UtcNow);
        await repository.UpdateAsync(request);

        var updated = await repository.GetByIdAsync(request.Id);

        // Assert
        updated.Should().NotBeNull();
        updated.Status.Should().Be(RequestStatus.Approved);
        updated.FinishDate.Should().NotBeNull();
    }

    [Fact]
    public async Task ListByCreatorIdAsync_ShouldReturnRequestsForCreator()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new ReceivableAdvanceRequestRepository(context);
        var creatorId = Guid.NewGuid();

        var requests = _fixture
            .Build<ReceivableAdvanceRequest>()
            .With(r => r.CreatorId, creatorId)
            .CreateMany<ReceivableAdvanceRequest>();

        foreach (var request in requests)
        {
            await repository.InsertAsync(request);
        }

        // Act
        var list = await repository.ListByCreatorIdAsync(creatorId);

        // Assert
        list.Should().NotBeNull();
        list.Count().Should().Be(requests.Count());
    }
}
