using System.Management.Automation;

namespace FastWildcard.Performance.Matchers
{
    public class AutomationWildcardMatcher
    {
        private readonly WildcardPattern _wildcardPattern;

        public AutomationWildcardMatcher(string pattern, WildcardOptions options)
        {
            _wildcardPattern = new WildcardPattern(pattern, options);
        }

        public bool Match(string str)
        {
            return _wildcardPattern.IsMatch(str);
        }
    }
}
