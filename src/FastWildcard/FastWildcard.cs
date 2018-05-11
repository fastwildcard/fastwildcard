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

            // Collapse repeated wildcard characters
            /*
            var patternBuilder = new StringBuilder(inputPattern.Length);
            var lastCh = inputPattern[0];
            patternBuilder.Append(lastCh);
            for (var inputIndex = 1; inputIndex < inputPattern.Length; inputIndex++)
            {
                var inputCh = inputPattern[inputIndex];

                if (lastCh == '*' && (inputCh == '*' || inputCh == '?'))
                {
                    continue;
                }

                if (lastCh == '?' && inputCh == '*')
                {
                    patternBuilder.Remove(patternBuilder.Length - 1, 1);
                }

                patternBuilder.Append(inputCh);
                lastCh = inputCh;
            }
            var pattern = patternBuilder.ToString();
            */

            // Multi character wildcard matches everything
            if (pattern == "*")
            {
                return true;
            }

            // Empty string does not match
            if (str == "")
            {
                return false;
            }

            // If pattern is longer than string, it cannot match
            if (pattern.Length > str.Length)
            {
                return false;
            }

            var strIndex = 0;
            for (var patternIndex = 0; patternIndex < pattern.Length; patternIndex++)
            {
                var patternCh = pattern[patternIndex];

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

                var skipToString = pattern.Substring(patternIndex + 1, skipStringEndIndex - patternIndex);

                var skipToStringIndex = str.IndexOf(skipToString, strIndex + 1, StringComparison.Ordinal);
                if (skipToStringIndex == -1)
                {
                    return false;
                }

                strIndex = skipToStringIndex;
                if (strIndex == str.Length)
                {
                    return true;
                }
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
