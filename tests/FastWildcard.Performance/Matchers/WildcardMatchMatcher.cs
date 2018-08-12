#if !NETCOREAPP
using WildcardMatch;

namespace FastWildcard.Performance.Matchers
{
    public class WildcardMatchMatcher
    {
        public bool Match(string pattern, string str)
        {
            return pattern.WildcardMatch(str);
        }
    }
}
#endif
