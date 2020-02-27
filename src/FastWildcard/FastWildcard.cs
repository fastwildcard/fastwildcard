using System;
using System.Collections.Generic;

namespace FastWildcard
{
    public class FastWildcard
    {
        private const char SingleWildcardCharacter = '?';
        private const char MultiWildcardCharacter = '*';

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
        /// Returns if the input string <paramref name="inputString"/> matches the given wildcard pattern <paramref name="wildcardPattern"/>.
        /// Uses the supplied <see cref="MatchSettings"/>.
        /// </summary>
        /// <param name="inputString">Input string to match on</param>
        /// <param name="wildcardPattern">Wildcard pattern to match with</param>
        /// <param name="matchSettings">Match settings to use</param>
        /// <returns>True if a match is found, false otherwise</returns>
        public static bool IsMatch(string inputString, string wildcardPattern, MatchSettings matchSettings)
        {
            // Pattern must contain something
            if (String.IsNullOrEmpty(wildcardPattern))
            {
                throw new ArgumentOutOfRangeException(nameof(wildcardPattern));
            }

            // Uninitialised string never matches
            if (inputString == null)
            {
                throw new ArgumentNullException(nameof(inputString));
            }

            // Empty string does not match
            if (inputString.Length == 0)
            {
                return false;
            }

            var result = true;

#if (NETSTANDARD || NETCOREAPP) && !NETSTANDARD1_3 && !NETSTANDARD2_0
            var str = inputString.AsSpan();
            var ptt = wildcardPattern.AsSpan();

            var multiWildcardIndex = 0;
            do
            {
                var nextMultiWildcardIndex = ptt.IndexOf(MultiWildcardCharacter);

                var compareToIndexStart = nextMultiWildcardIndex == -1 ? str.Length - 1 : nextMultiWildcardIndex;
                if (compareToIndexStart - multiWildcardIndex == 0)
                {
                    break;
                }

                var pttSlice = ptt.Slice(multiWildcardIndex, nextMultiWildcardIndex);
                if (!CompareByCharacterAndSingleWildcardMatches(
                        str.Slice(multiWildcardIndex, nextMultiWildcardIndex),
                        pttSlice,
                        matchSettings.StringComparison))
                {
                    return false;
                }

                bool greedyMatches;
                do
                {
                    greedyMatches = CompareByCharacterAndSingleWildcardMatches(
                        str.Slice(multiWildcardIndex, nextMultiWildcardIndex),
                        pttSlice,
                        matchSettings.StringComparison);
                } while (greedyMatches);
                
                multiWildcardIndex = nextMultiWildcardIndex + 1;
            } while (multiWildcardIndex != 0);


#else
            var str = inputString;
            var ptt = wildcardPattern;
            var strIndexStart = 0;
            var pttIndexStart = 0;
            var compareLength = ptt.Length;
            List<int> multiWildcardIndexes = null;

            int compareIndex;
            do
            {
                compareIndex = CompareByCharacterAndSingleWildcardMatches(
                    str,
                    ptt,
                    matchSettings.StringComparison,
                    strIndexStart,
                    pttIndexStart,
                    compareLength
                );

                if (compareIndex == -1)
                {
                    return false;
                }

                if (compareIndex > 0)
                {
                    var patternCh = ptt[pttIndexStart + compareIndex];
                    if (patternCh != MultiWildcardCharacter)
                    {
                        return false;
                    }

                    if (multiWildcardIndexes == null)
                    {
                        multiWildcardIndexes = GetMultiWildcardIndexes(ptt);
                    }
                    var endMultiWildcardIndex = pttIndexStart + compareIndex;
                    var startMultiWildcardIndex = multiWildcardIndexes[multiWildcardIndexes.FindLastIndex(p => p == endMultiWildcardIndex) - 1];
                    compareLength = endMultiWildcardIndex - startMultiWildcardIndex;
                    strIndexStart = strIndexStart - compareLength;
                    pttIndexStart = startMultiWildcardIndex + 1;
                }

            } while (compareIndex != 0);



            /*

            var multiWildcardIndexes = new List<int>();

            var multiWildcardIndex = ptt.IndexOf(MultiWildcardCharacter);

            while (multiWildcardIndex != -1)
            {
                multiWildcardIndexes.Add(multiWildcardIndex);
                multiWildcardIndex = ptt.IndexOf(MultiWildcardCharacter, multiWildcardIndex + 1);
            }

            if (multiWildcardIndexes.Count == 0)
            {
                return 
            }


            var strIndexStart = 0;
            var pttIndexStart = 0;
            var compareLength = multiWildcardIndexes[0];
            for (var ii = 1; ii <= multiWildcardIndexes.Count; ii++)
            {
                result = CompareByCharacterAndSingleWildcardMatches(
                    str,
                    ptt,
                    matchSettings.StringComparison,
                    strIndexStart,
                    pttIndexStart,
                    compareLength
                );

                if (ii == multiWildcardIndexes.Count)
                {
                    // Test end of string and then work backwards if fails
                    // TODO: This for loop should go in reverse
                }

                multiWildcardIndex = multiWildcardIndexes[ii - 1];
                strIndexStart += compareLength + 1;
                pttIndexStart = multiWildcardIndex + 1;
                compareLength = multiWildcardIndexes[ii] - multiWildcardIndex - 1;
            }
            */





            //var compareToIndexStart = nextMultiWildcardIndex == -1 ? ptt.Length - 1 : nextMultiWildcardIndex - 1;
            //if (compareToIndexStart - multiWildcardIndex == 0)
            //{
            //    break;
            //}

            //var compareLength = compareToIndexStart - pttIndexStart;


            //if (!result)
            //{
            //    strIndexStart++;
            //    pttIndexStart++;
            //}
            //else
            //{
            //    greedyMode = true;
            //}

            //pttIndexStart = nextMultiWildcardIndex + 1;
#endif

            return result;
        }

        private static List<int> GetMultiWildcardIndexes(string ptt)
        {
            var multiWildcardIndexes = new List<int>();

            var multiWildcardIndex = ptt.IndexOf(MultiWildcardCharacter);

            while (multiWildcardIndex != -1)
            {
                multiWildcardIndexes.Add(multiWildcardIndex);
                multiWildcardIndex = ptt.IndexOf(MultiWildcardCharacter, multiWildcardIndex + 1);
            }

            return multiWildcardIndexes;
        }

#if (NETSTANDARD || NETCOREAPP) && !NETSTANDARD1_3 && !NETSTANDARD2_0
        private static bool CompareByCharacterAndSingleWildcardMatches(
            ReadOnlySpan<char> str,
            ReadOnlySpan<char> pattern,
            StringComparison comparisonMethod
        )
        {
            var compareFromIndex = 0;
            var compareToIndex = pattern.IndexOf(SingleWildcardCharacter);
            if (compareToIndex == -1)
            {
                compareToIndex = pattern.Length - 1;
            }

            for (;;)
            {
                var strComparison = str.Slice(compareFromIndex, compareToIndex);
                var patternComparison = pattern.Slice(compareFromIndex, compareToIndex);
                if (!strComparison.Equals(patternComparison, comparisonMethod))
                {
                    return false;
                }

                compareFromIndex = compareToIndex + 1;
                if (compareFromIndex == pattern.Length)
                {
                    return true;
                }

                patternComparison = pattern.Slice(compareFromIndex);
                compareToIndex = patternComparison.IndexOf(SingleWildcardCharacter);
                if (compareToIndex == -1)
                {
                    compareToIndex = pattern.Length - 1;
                }
            }
        }
#else
        private static int CompareByCharacterAndSingleWildcardMatches(
            string str,
            string ptt,
            StringComparison comparisonMethod,
            int strIndexStart,
            int pttIndexStart,
            int length
        )
        {
            if (strIndexStart + length > str.Length ||
                pttIndexStart + length > ptt.Length)
            {
                return -1;
            }

            for (var compareIndex = length - 1; compareIndex >= 0; compareIndex--)
            {
                var patternCh = ptt[pttIndexStart + compareIndex];

                if (patternCh == SingleWildcardCharacter)
                {
                    continue;
                }

                var strCh = str[strIndexStart + compareIndex];

                var chEquals = comparisonMethod == StringComparison.Ordinal
                    ? patternCh.Equals(strCh)
                    : patternCh.ToString().Equals(strCh.ToString(), comparisonMethod);
                if (chEquals)
                {
                    continue;
                }

                return compareIndex;
            }

            return 0;
        }
#endif
    }
}
