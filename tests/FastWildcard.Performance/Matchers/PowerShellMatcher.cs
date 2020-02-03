#if NETCOREAPP
using System.Management.Automation;

namespace FastWildcard.Performance.Matchers
{
    public class PowerShellMatcher
    {
        private readonly WildcardPattern _wildcardPattern;

        public PowerShellMatcher(string pattern, WildcardOptions options)
        {
            // Adapted from https://stackoverflow.com/a/6907849/6651
            _wildcardPattern = new WildcardPattern(pattern, options);
        }

        public bool Match(string str)
        {
            return _wildcardPattern.IsMatch(str);
        }
    }
}
#endif
