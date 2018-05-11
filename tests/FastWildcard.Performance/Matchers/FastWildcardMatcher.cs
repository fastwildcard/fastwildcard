namespace FastWildcard.Performance.Matchers
{
    public class FastWildcardMatcher
    {
        public bool Match(string str, string pattern)
        {
            return FastWildcard.IsMatch(str, pattern);
        }
    }
}
