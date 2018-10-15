using System;
using System.ComponentModel.DataAnnotations;
using AutoFixture.Xunit2;
using FastWildcard.Performance.Benchmarks;
using FluentAssertions;
using Xunit;

namespace FastWildcard.Performance.Tests
{
    public class IterationBuilderTests
    {
        [Theory, AutoData]
        public void BuildTestString_InvalidParameter_ThrowsException([Range(-100, 0)] int stringLength)
        {
            Action action = () => IterationBuilder.BuildTestString(stringLength);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory, AutoData]
        public void BuildTestString_OfLength_ReturnsString([Range(1, 100)] int stringLength)
        {
            var result = IterationBuilder.BuildTestString(stringLength);

            result.Should().NotBeNullOrWhiteSpace();
        }
    }
}
