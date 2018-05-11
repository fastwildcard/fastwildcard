using System;

namespace FastWildcard
{
    public class FastWildcard
    {
        /// <summary>
        /// Returns if the input string <paramref name="str"/> matches the given wildcard pattern <paramref name="pattern"/>.
        /// </summary>
        /// <param name="str">Input string to match on</param>
        /// <param name="pattern">Wildcard pattern to use</param>
        /// <returns>True if a match is found, false otherwise</returns>
        public static bool IsMatch(string str, string pattern)
        {
            if (String.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentOutOfRangeException(nameof(pattern));
            }

            if (str == null)
            {
                return false;
            }

            if (pattern == "*")
            {
                return true;
            }

            if (str == "")
            {
                return false;
            }

            if (pattern.Length > str.Length)
            {
                return false;
            }

            var strIndex = 0;
            for (var patternIndex = 0; patternIndex < pattern.Length; patternIndex++)
            {
                var patternCh = pattern[patternIndex];

                //// Character match
                if (patternCh == str[strIndex])
                {
                    strIndex++;
                    continue;
                }

                //// Single wildcard match
                if (patternCh == '?')
                {
                    strIndex++;
                    continue;
                }

                //// No match
                if (patternCh != '*')
                {
                    return false;
                }

                //// Multi character wildcard - last character in the pattern
                if (patternIndex == pattern.Length - 1)
                {
                    return true;
                }

                //// Multi character wildcard match - general case

                // Skip '?' if followed by '*'
                var endMultiCharacterWildcardCh = pattern[patternIndex + 1];
                while (endMultiCharacterWildcardCh == '?')
                {
                    patternIndex++;

                    // End of string is '*?'
                    if (patternIndex + 1 >= pattern.Length)
                    {
                        return true;
                    }

                    endMultiCharacterWildcardCh = pattern[patternIndex + 1];
                }

                // Skip until reach next real character
                do
                {
                    strIndex++;
                    if (strIndex == str.Length)
                    {
                        return false;
                    }
                } while (str[strIndex] != endMultiCharacterWildcardCh);
            }

            if (strIndex < str.Length)
                return false;
            return true;
        }
    }
}
