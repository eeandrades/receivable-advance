using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ReceivableAdvance.Application.Commands.FinishReceivableAdvanceRequests;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using Xunit;
using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Tests.Commands.FinishReceivableAdvanceRequests;

public class FinishReceivableAdvanceRequestHandlerTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldApproveRequest_WhenRequestExists()
    {
        // Arrange
        var repositoryMock = new Mock<IReceivableAdvanceRequestRepository>();
        var requestId = Guid.NewGuid();
        var creatorId = Guid.NewGuid();
        var requestValue = new Money(1000m);
        var netValue = new Money(950m);
        var requestDate = DateTime.UtcNow;
        var status = RequestStatus.Pending;
        DateTime? finishDate = null;

        var request = new ReceivableAdvanceRequest(
            requestId,
            creatorId,
            requestValue,
            netValue,
            requestDate,
            status,
            finishDate
        );

        var command = new ApproveReceivableAdvanceRequestCommand(requestId);

        repositoryMock.Setup(r => r.GetByIdAsync(command.RequestId))
            .ReturnsAsync(request);

        var handler = new FinishReceivableAdvanceRequestHandler(repositoryMock.Object);

        // Act
        var result = await handler.ExecuteAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
        repositoryMock.Verify(r => r.UpdateAsync(request), Times.Once);
        request.Status.Should().Be(RequestStatus.Approved);
        request.FinishDate.Should().NotBeNull();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldRejectRequest_WhenRequestExists()
    {
        // Arrange
        var repositoryMock = new Mock<IReceivableAdvanceRequestRepository>();
        var requestId = Guid.NewGuid();
        var creatorId = Guid.NewGuid();
        var requestValue = new Money(1000m);
        var netValue = new Money(950m);
        var requestDate = DateTime.UtcNow;
        var status = RequestStatus.Pending;
        DateTime? finishDate = null;

        var request = new ReceivableAdvanceRequest(
            requestId,
            creatorId,
            requestValue,
            netValue,
            requestDate,
            status,
            finishDate
        );

        var command = new RejectReceivableAdvanceRequestCommand(requestId);

        repositoryMock.Setup(r => r.GetByIdAsync(command.RequestId))
            .ReturnsAsync(request);

        var handler = new FinishReceivableAdvanceRequestHandler(repositoryMock.Object);

        // Act
        var result = await handler.ExecuteAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
        repositoryMock.Verify(r => r.UpdateAsync(request), Times.Once);
        request.Status.Should().Be(RequestStatus.Rejected);
        request.FinishDate.Should().NotBeNull();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNotFound_WhenRequestDoesNotExist()
    {
        // Arrange
        var repositoryMock = new Mock<IReceivableAdvanceRequestRepository>();
        var requestId = Guid.NewGuid();

        var approveCommand = new ApproveReceivableAdvanceRequestCommand(requestId);
        var rejectCommand = new RejectReceivableAdvanceRequestCommand(requestId);

        repositoryMock.Setup(r => r.GetByIdAsync(approveCommand.RequestId))
            .ReturnsAsync((ReceivableAdvanceRequest?)null);

        var handler = new FinishReceivableAdvanceRequestHandler(repositoryMock.Object);

        // Act
        var approveResult = await handler.ExecuteAsync(approveCommand);
        var rejectResult = await handler.ExecuteAsync(rejectCommand);

        // Assert
        approveResult.IsValid.Should().BeFalse();
        approveResult.Notification.Should().BeOfType<ReceivableAdvanceRequestNotFound>();
        rejectResult.IsValid.Should().BeFalse();
        rejectResult.Notification.Should().BeOfType<ReceivableAdvanceRequestNotFound>();
    }
    [Fact]
    public async Task ExecuteAsync_ShouldReturnAlreadyFinished_WhenApprovingAlreadyFinishedRequest()
    {
        // Arrange
        var repositoryMock = new Mock<IReceivableAdvanceRequestRepository>();
        var requestId = Guid.NewGuid();
        var creatorId = Guid.NewGuid();
        var requestValue = new Money(1000m);
        var netValue = new Money(950m);
        var requestDate = DateTime.UtcNow;
        var status = RequestStatus.Approved;
        var finishDate = DateTime.UtcNow.AddDays(-1);

        var request = new ReceivableAdvanceRequest(
            requestId,
            creatorId,
            requestValue,
            netValue,
            requestDate,
            status,
            finishDate
        );

        var command = new ApproveReceivableAdvanceRequestCommand(requestId);

        repositoryMock.Setup(r => r.GetByIdAsync(command.RequestId))
            .ReturnsAsync(request);

        var handler = new FinishReceivableAdvanceRequestHandler(repositoryMock.Object);

        // Act
        var result = await handler.ExecuteAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Notification.Should().BeOfType<RequestAlreadyFinished>();
        repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ReceivableAdvanceRequest>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnAlreadyFinished_WhenRejectingAlreadyFinishedRequest()
    {
        // Arrange
        var repositoryMock = new Mock<IReceivableAdvanceRequestRepository>();
        var requestId = Guid.NewGuid();
        var creatorId = Guid.NewGuid();
        var requestValue = new Money(1000m);
        var netValue = new Money(950m);
        var requestDate = DateTime.UtcNow;
        var status = RequestStatus.Rejected;
        var finishDate = DateTime.UtcNow.AddDays(-1);

        var request = new ReceivableAdvanceRequest(
            requestId,
            creatorId,
            requestValue,
            netValue,
            requestDate,
            status,
            finishDate
        );

        var command = new RejectReceivableAdvanceRequestCommand(requestId);

        repositoryMock.Setup(r => r.GetByIdAsync(command.RequestId))
            .ReturnsAsync(request);

        var handler = new FinishReceivableAdvanceRequestHandler(repositoryMock.Object);

        // Act
        var result = await handler.ExecuteAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Notification.Should().BeOfType<RequestAlreadyFinished>();
        repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ReceivableAdvanceRequest>()), Times.Never);
    }
}