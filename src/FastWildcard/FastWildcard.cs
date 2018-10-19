using System;

namespace FastWildcard
{
    public class FastWildcard
    {
        private static readonly char[] WildcardCharacters = {'?', '*'};

        /// <summary>
        /// Returns if the input string <paramref name="str"/> matches the given wildcard pattern <paramref name="pattern"/>.
        /// </summary>
        /// <param name="str">Input string to match on</param>
        /// <param name="pattern">Wildcard pattern to use</param>
        /// <returns>True if a match is found, false otherwise</returns>
        public static bool IsMatch(string str, string pattern)
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

            var strIndex = 0;
            for (var patternIndex = 0; patternIndex < pattern.Length; patternIndex++)
            {
                var patternCh = pattern[patternIndex];

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
                if (patternCh == str[strIndex])
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
                var skipToString = pattern.Substring(patternIndex + 1, skipStringEndIndex - patternIndex);
                skipToStringIndex = str.IndexOf(skipToString, strIndex, StringComparison.Ordinal);
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
