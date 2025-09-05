using System.Threading.Tasks;
using FluentAssertions;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using Xunit;

namespace ReceivableAdvance.Tests.Aggreegates.ReceivableAdvanceRequests;

public class StandardReceivableAdvanceFeePolicyTests
{
    [Fact]
    public async Task GetFeeFor_ShouldReturnStandardFee()
    {
        // Arrange
        var policy = new StandardReceivableAdvanceFeePolicy();
        var args = new ReceivableAdvanceFeePolicyArgs(
            CreatorId: Guid.NewGuid(),
            RequestValue: new Money(1000m),
            RequestDate: DateTime.UtcNow);

        // Act
        var fee = await policy.GetFeeFor(args);

        // Assert
        fee.Value.Should().Be(0.05m); // 5% as fraction
    }
}