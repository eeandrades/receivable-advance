using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using Xunit;

public class ReceivableAdvanceRequestTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task CreatePending_ShouldSetStatusToPending_AndCalculateNetValue()
    {
        // Arrange
        var creatorId = _fixture.Create<Guid>();
        var requestValue = new Money(_fixture.Create<decimal>());
        var fee = Percentage.FromPercent(1);
        var feePolicyMock = new Mock<IReceivableAdvanceFeePolicy>();
        feePolicyMock
            .Setup(x => x.GetFeeFor(It.IsAny<ReceivableAdvanceFeePolicyArgs>()))
            .ReturnsAsync(fee);

        // Act
        var request = await ReceivableAdvanceRequest.CreatePending(creatorId, requestValue, feePolicyMock.Object);

        // Assert
        request.CreatorId.Should().Be(creatorId);
        request.RequestValue.Should().Be(requestValue);
        request.Status.Should().Be(RequestStatus.Pending);
        request.NetValue.Should().Be(requestValue * (1 - fee));
        request.FinishDate.Should().BeNull();
    }

    [Fact]
    public void Approve_ShouldSetStatusToApproved_AndSetFinishDate()
    {
        // Arrange
        var approveDate = _fixture.Create<DateTime>();
        var request = new ReceivableAdvanceRequest(
            _fixture.Create<Guid>(),
            _fixture.Create<Guid>(),
            new Money(1000m),
            new Money(900m),
            _fixture.Create<DateTime>(),
            RequestStatus.Pending,
            null);

        // Act
        var notification = request.Approve(approveDate);

        // Assert
        request.Status.Should().Be(RequestStatus.Approved);
        request.FinishDate.Should().Be(approveDate);
        notification.Should().BeOfType<RequestFinishedSuccess>();
    }

    [Fact]
    public void Reject_ShouldSetStatusToRejected_AndSetFinishDate()
    {
        // Arrange
        var rejectDate = _fixture.Create<DateTime>();
        var request = new ReceivableAdvanceRequest(
            _fixture.Create<Guid>(),
            _fixture.Create<Guid>(),
            new Money(1000m),
            new Money(900m),
            _fixture.Create<DateTime>(),
            RequestStatus.Pending,
            null);

        // Act
        var notification = request.Reject(rejectDate);

        // Assert
        request.Status.Should().Be(RequestStatus.Rejected);
        request.FinishDate.Should().Be(rejectDate);
        notification.Should().BeOfType<RequestFinishedSuccess>();
    }

    [Fact]
    public void Approve_ShouldReturnAlreadyFinishedNotification_IfNotPending()
    {
        // Arrange
        var request = new ReceivableAdvanceRequest(
            _fixture.Create<Guid>(),
            _fixture.Create<Guid>(),
            new Money(1000m),
            new Money(900m),
            _fixture.Create<DateTime>(),
            RequestStatus.Approved,
            _fixture.Create<DateTime>());

        // Act
        var notification = request.Approve(_fixture.Create<DateTime>());

        // Assert
        notification.Should().BeOfType<RequestAlreadyFinished>();
    }

    [Fact]
    public void Reject_ShouldReturnAlreadyFinishedNotification_IfNotPending()
    {
        // Arrange
        var request = new ReceivableAdvanceRequest(
            _fixture.Create<Guid>(),
            _fixture.Create<Guid>(),
            new Money(1000m),
            new Money(900m),
            _fixture.Create<DateTime>(),
            RequestStatus.Approved,
            _fixture.Create<DateTime>());

        // Act
        var notification = request.Reject(_fixture.Create<DateTime>());

        // Assert
        notification.Should().BeOfType<RequestAlreadyFinished>();
    }
}