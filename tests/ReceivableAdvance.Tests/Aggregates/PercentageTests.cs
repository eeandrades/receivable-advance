using System;
using AutoFixture;
using FluentAssertions;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using Xunit;

namespace ReceivableAdvance.Tests.Aggreegates.ReceivableAdvanceRequests;

public class PercentageTests
{
    private readonly Fixture _fixture = new();

    [Theory]
    [InlineData(0)]
    [InlineData(0.5)]
    [InlineData(1)]
    public void Constructor_ShouldSetValue_WhenInRange(decimal value)
    {
        // Act
        var percentage = new Percentage(value);

        // Assert
        percentage.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(1.01)]
    public void Constructor_ShouldThrow_WhenOutOfRange(decimal value)
    {
        // Act
        Action act = () => new Percentage(value);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void FromPercent_ShouldConvertPercentToFraction()
    {
        // Arrange
        decimal percent = 25;

        // Act
        var percentage = Percentage.FromPercent(percent);

        // Assert
        percentage.Value.Should().Be(0.25m);
    }

    [Fact]
    public void ImplicitConversionToDecimal_ShouldReturnValue()
    {
        // Arrange
        var percentage = new Percentage(0.75m);

        // Act
        decimal value = percentage;

        // Assert
        value.Should().Be(0.75m);
    }
}