namespace FastWildcard
{
    public class FastWildcard
    {
        public static bool IsMatch(string pattern, string str)
        {
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

                // Multi character wildcard as last character in the pattern
                if (patternIndex == pattern.Length - 1)
                {
                    return true;
                }

                // Multi wildcard match
                var endMultiCharacterWildcardCh = pattern[patternIndex + 1];
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
