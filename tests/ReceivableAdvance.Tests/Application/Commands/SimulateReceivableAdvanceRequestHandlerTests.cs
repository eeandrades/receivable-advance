using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ReceivableAdvance.Application.Commands.SimulateReceivableAdvanceRequests;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using Xunit;
 
namespace ReceivableAdvance.Tests.Application.Commands.SimulateReceivableAdvanceRequests;

public class SimulateReceivableAdvanceRequestHandlerTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldSimulateRequest_WhenCommandIsValid()
    {
        // Arrange
        var feePolicyMock = new Mock<IReceivableAdvanceFeePolicy>();
        feePolicyMock.Setup(fp => fp.GetFeeFor(It.IsAny<ReceivableAdvanceFeePolicyArgs>()))
                     .ReturnsAsync(0.1m); 
        var handler = new SimulateReceivableAdvanceRequestHandler(feePolicyMock.Object);
        var creatorId = Guid.NewGuid();
        var amount = 1000m;
        var command = new SimulateReceivableAdvanceRequestCommand(creatorId, amount);

        // Act
        var result = await handler.ExecuteAsync(command);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();       
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnErrorNotification_WhenCommandIsInvalid()
    {
        // Arrange
        var feePolicyMock = new Mock<IReceivableAdvanceFeePolicy>();
        feePolicyMock.Setup(fp => fp.GetFeeFor(It.IsAny<ReceivableAdvanceFeePolicyArgs>()))
                     .ReturnsAsync(0.2m);
        var handler = new SimulateReceivableAdvanceRequestHandler(feePolicyMock.Object);
        var creatorId = Guid.Empty; // Invalid Guid
        var amount = -100m; // Invalid amount
        var command = new SimulateReceivableAdvanceRequestCommand(creatorId, amount);

        // Act
        var result = await handler.ExecuteAsync(command);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();  
        result.Notification.Should().BeOfType<InvalidRequestAmount>();
    }
}