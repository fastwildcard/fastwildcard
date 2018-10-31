using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace FastWildcard.Tests
{
    public class MatchSettingsTests
    {
        [Fact]
        public void StringComparison_Default_IsOrdinal()
        {
            var matchSettings = new MatchSettings();

            matchSettings.StringComparison.Should().Be(StringComparison.Ordinal);
        }

        [Theory]
        [MemberData(nameof(StringComparisonEnums))]
        public void StringComparison_Set_IsSet(StringComparison setting)
        {
            var matchSettings = new MatchSettings
            {
                StringComparison = setting
            };

            matchSettings.StringComparison.Should().Be(setting);
        }

        public static IEnumerable<object[]> StringComparisonEnums()
        {
            foreach (StringComparison stringComparisonEnum in Enum.GetValues(typeof(StringComparison)))
            {
                yield return new object[] { stringComparisonEnum };
            }
        }
    }
}
