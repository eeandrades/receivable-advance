using AutoFixture;
using FluentAssertions;
using Moq;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Application.Queries.ListReceivableAdvanceRequests;
using ReceivableAdvance.Common.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ReceivableAdvance.Tests.Application.Queries;

public class ListReceivableAdvanceRequestHandlerTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IReceivableAdvanceRequestRepository> _repositoryMock = new();
    private readonly ListReceivableAdvanceRequestHandler _handler;

    public ListReceivableAdvanceRequestHandlerTests()
    {
        _handler = new ListReceivableAdvanceRequestHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSuccessResult_WhenRequestsExist()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var requests = _fixture.CreateMany<ReceivableAdvanceRequest>().ToList();
        _repositoryMock
            .Setup(r => r.ListByCreatorIdAsync(creatorId))
            .ReturnsAsync(requests);

        var query = new ListReceivableAdvanceRequestQuery(creatorId);

        // Act
        var result = await _handler.ExecuteAsync(query);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Count().Should().Be(requests.Count);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNotFoundResult_WhenNoRequestsExist()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.ListByCreatorIdAsync(creatorId))
            .ReturnsAsync([]);

        var query = new ListReceivableAdvanceRequestQuery(creatorId);

        // Act
        var result = await _handler.ExecuteAsync(query);

        // Assert
        result.IsValid .Should().BeFalse();
        result.Notification.Should().BeOfType<ReceivableAdvanceRequestNotFound>();
        result.Notification.Should().BeOfType<ReceivableAdvanceRequestNotFound>();
    }
}

