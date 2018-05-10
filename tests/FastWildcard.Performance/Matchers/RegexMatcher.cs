using System.Text.RegularExpressions;

namespace FastWildcard.Performance.Matchers
{
    public class RegexMatcher
    {
        private readonly Regex _regex;

        public RegexMatcher(string pattern, RegexOptions options)
        {
            // Adapted from https://stackoverflow.com/a/6907849/6651
            _regex = new Regex("^"
                               + Regex.Escape(pattern)
                                   .Replace(@"\*", ".*")
                                   .Replace(@"\?", ".")
                               + "$",
                options);
        }

        public bool Match(string str)
        {
            return _regex.IsMatch(str);
        }
    }
}
