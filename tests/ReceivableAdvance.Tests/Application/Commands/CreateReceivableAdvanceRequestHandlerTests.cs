using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using Xunit;
using AutoFixture;

namespace ReceivableAdvance.Application.Tests.Commands.CreateReceivableAdvanceRequests;

public class CreateReceivableAdvanceRequestHandlerTests
{
    private Fixture _fixture = new ();
    [Fact]
    public async Task ExecuteAsync_ShouldCreateRequest_WhenCommandIsValid()
    {
        // Arrange
        var repositoryMock = new Mock<IReceivableAdvanceRequestRepository>();
        var feePolicyMock = new Mock<IReceivableAdvanceFeePolicy>();
        feePolicyMock.Setup(fp => fp.GetFeeFor(It.IsAny<ReceivableAdvanceFeePolicyArgs>()))
                     .ReturnsAsync(0.1m);

        var handler = new CreateReceivableAdvanceRequestHandler(repositoryMock.Object, feePolicyMock.Object);

        var creatorId = Guid.NewGuid();
        var amount = 1000m;
        var command = new CreateReceivableAdvanceRequestCommand(creatorId, amount);

        repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ReceivableAdvanceRequest>()));

        // Act
        var result = await handler.ExecuteAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
        repositoryMock.Verify(r => r.InsertAsync(It.IsAny<ReceivableAdvanceRequest>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnInvalidRequestAmountNotification_WhenAmountIsInvalid()
    {
        // Arrange
        var repositoryMock = new Mock<IReceivableAdvanceRequestRepository>();
        var feePolicyMock = new Mock<IReceivableAdvanceFeePolicy>();
        var handler = new CreateReceivableAdvanceRequestHandler(repositoryMock.Object, feePolicyMock.Object);

        var creatorId = Guid.NewGuid();
        var invalidAmount = -500m; // Invalid amount
        var command = new CreateReceivableAdvanceRequestCommand(creatorId, invalidAmount);

        // Act
        var result = await handler.ExecuteAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Notification.Should().BeOfType<InvalidRequestAmount>();
    }
    [Fact]
    public async Task ExecuteAsync_ShouldReturnAlreadyExistsNotification_WhenReceivableAdvanceRequestPendingAlreadyExists()
    {
        // Arrange
        var repositoryMock = new Mock<IReceivableAdvanceRequestRepository>();

        // Simulate that a request already exists for this creatorId and amount
        repositoryMock
            .Setup(r => r.GetPendingByCreatorAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_fixture.Create<ReceivableAdvanceRequest>());

        var feePolicyMock = new Mock<IReceivableAdvanceFeePolicy>();
        var handler = new CreateReceivableAdvanceRequestHandler(repositoryMock.Object, feePolicyMock.Object);

        var creatorId = Guid.NewGuid();
        var amount = 1000m;
        var command = new CreateReceivableAdvanceRequestCommand(creatorId, amount);

        // Act
        var result = await handler.ExecuteAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Notification.Should().BeOfType<ReceivableAdvanceRequestPendingAlreadyExists>();
    }
}