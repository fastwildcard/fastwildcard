using System;

namespace FastWildcard
{
    public class FastWildcard
    {
        private const char SingleWildcardCharacter = '?';
        private const char MultiWildcardCharacter = '*';

        private static readonly char[] WildcardCharacters = {SingleWildcardCharacter, MultiWildcardCharacter};

        /// <summary>
        /// Returns if the input string <paramref name="str"/> matches the given wildcard pattern <paramref name="pattern"/>.
        /// Uses default <see cref="MatchSettings"/>.
        /// </summary>
        /// <param name="str">Input string to match on</param>
        /// <param name="pattern">Wildcard pattern to match against</param>
        /// <returns>True if a match is found, false otherwise</returns>
        public static bool IsMatch(string str, string pattern)
        {
            return IsMatch(str, pattern, new MatchSettings());
        }

        /// <summary>
        /// Returns if the input string <paramref name="str"/> matches the given wildcard pattern <paramref name="pattern"/>.
        /// Uses the supplied <see cref="MatchSettings"/>.
        /// </summary>
        /// <param name="str">Input string to match on</param>
        /// <param name="pattern">Wildcard pattern to match with</param>
        /// <param name="matchSettings">Match settings to use</param>
        /// <returns>True if a match is found, false otherwise</returns>
        public static bool IsMatch(string str, string pattern, MatchSettings matchSettings)
        {
            // Pattern must contain something
            if (String.IsNullOrEmpty(pattern))
            {
                throw new ArgumentOutOfRangeException(nameof(pattern));
            }

            // Uninitialised string never matches
            if (str == null)
            {
                return false;
            }

            // Multi character wildcard matches everything
            if (pattern == "*")
            {
                return true;
            }

            // Empty string does not match
            if (str.Length == 0)
            {
                return false;
            }

#if (NETSTANDARD || NETCOREAPP) && !NETSTANDARD1_3 && !NETSTANDARD2_0
            var strSpan = str.AsSpan();
            var patternSpan = pattern.AsSpan();
#endif

            var strIndex = 0;

            for (var patternIndex = 0; patternIndex < pattern.Length; patternIndex++)
            {
                var patternCh = pattern[patternIndex];
#if (NETSTANDARD || NETCOREAPP) && !NETSTANDARD1_3 && !NETSTANDARD2_0
                var patternChSpan = patternSpan.Slice(patternIndex, 1);
#endif

                if (strIndex >= str.Length)
                {
                    // At end of pattern for this longer string so always matches '*'
                    if (patternCh == '*')
                    {
                        return true;
                    }

                    return false;
                }

                // Character match
#if (NETSTANDARD || NETCOREAPP) && !NETSTANDARD1_3 && !NETSTANDARD2_0
                var strAtIndex = strSpan.Slice(strIndex, 1);
                if (patternChSpan.Equals(strAtIndex, matchSettings.StringComparison))
#else
                var strAtIndex = str[strIndex].ToString();
                if (patternCh.ToString().Equals(strAtIndex, matchSettings.StringComparison))
#endif
                {
                    strIndex++;
                    continue;
                }

                // Single wildcard match
                if (patternCh == '?')
                {
                    strIndex++;
                    continue;
                }

                // No match
                if (patternCh != '*')
                {
                    return false;
                }

                // Multi character wildcard - last character in the pattern
                if (patternIndex == pattern.Length - 1)
                {
                    return true;
                }

                // Multi character wildcard match - general case
                var nextWildcardIndex = pattern.IndexOfAny(WildcardCharacters, patternIndex + 1);
                int skipStringEndIndex;
                if (nextWildcardIndex == -1)
                {
                    skipStringEndIndex = pattern.Length - 1;
                }
                else
                {
                    skipStringEndIndex = nextWildcardIndex - 1;
                }

                int skipToStringIndex;
                var skipToStringStartIndex = patternIndex + 1;
                var skipToStringLength = skipStringEndIndex - patternIndex;
#if (NETSTANDARD || NETCOREAPP) && !NETSTANDARD1_3 && !NETSTANDARD2_0
                var skipToString = patternSpan.Slice(skipToStringStartIndex, skipToStringLength);
                skipToStringIndex = strSpan.Slice(strIndex).IndexOf(skipToString, matchSettings.StringComparison) + strIndex;
#else
                var skipToString = pattern.Substring(skipToStringStartIndex, skipToStringLength);
                skipToStringIndex = str.IndexOf(skipToString, strIndex, matchSettings.StringComparison);
#endif
                if (skipToStringIndex == -1)
                {
                    return false;
                }

                strIndex = skipToStringIndex;
            }

            // Pattern processing completed but rest of input string was not
            if (strIndex < str.Length)
            {
                return false;
            }

            return true;
        }
    }
}
