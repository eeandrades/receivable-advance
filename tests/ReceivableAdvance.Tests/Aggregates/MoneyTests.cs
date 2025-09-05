using System;
using AutoFixture;
using FluentAssertions;
using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using Xunit;

namespace ReceivableAdvance.Tests.Aggreegates.ReceivableAdvanceRequests;

public class MoneyTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Constructor_ShouldRoundAmountToTwoDecimalPlaces()
    {
        // Arrange
        var value = 123.4567m;

        // Act
        var money = new Money(value);

        // Assert
        money.Amount.Should().Be(123.46m);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Constructor_ShouldThrowIfNegativeOrZero(decimal value)
    {
        // Act
        Action act = () => new Money(value);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void SubtractionOperator_ShouldSubtractAmounts()
    {
        // Arrange
        var a = new Money(100m);
        var b = new Money(40m);

        // Act
        var result = a - b;

        // Assert
        result.Amount.Should().Be(60m);
    }

    [Fact]
    public void MultiplicationOperator_ShouldMultiplyAmountByFactor()
    {
        // Arrange
        var a = new Money(50m);
        var factor = 2.5m;

        // Act
        var result = a * factor;

        // Assert
        result.Amount.Should().Be(125m);
    }

    [Fact]
    public void ImplicitConversionFromDecimal_ShouldCreateMoney()
    {
        // Arrange
        decimal value = 77.77m;

        // Act
        Money money = value;

        // Assert
        money.Amount.Should().Be(77.77m);
    }

    [Fact]
    public void ImplicitConversionToDecimal_ShouldReturnAmount()
    {
        // Arrange
        var money = new Money(88.88m);

        // Act
        decimal value = money;

        // Assert
        value.Should().Be(88.88m);
    }
}